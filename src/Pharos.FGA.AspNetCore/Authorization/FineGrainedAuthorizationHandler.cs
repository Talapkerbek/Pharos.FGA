using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenFga.Sdk.Client.Model;
using Pharos.FGA.AspNetCore.Authorization.Attributes;
using Pharos.FGA.AspNetCore.Authorization.Exceptions;

namespace Pharos.FGA.AspNetCore.Authorization;

internal sealed class FineGrainedAuthorizationHandler : AuthorizationHandler<FineGrainedAuthorizationRequirement>
{
    private readonly IFgaCheckDecorator _client;
    private readonly ILogger<FineGrainedAuthorizationHandler> _logger;

    public FineGrainedAuthorizationHandler(IFgaCheckDecorator client, ILogger<FineGrainedAuthorizationHandler> logger)
    {
        _client = client;
        _logger = logger;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, FineGrainedAuthorizationRequirement requirement)
    {
        if (context.Resource is not HttpContext httpContext)
            throw new InvalidOperationException($"{nameof(FineGrainedAuthorizationHandler)} called with invalid resource type. This handler is only compatible with endpoint routing.");
        
        var endpoint = httpContext.GetEndpoint();
        if (endpoint is null)
            throw new InvalidOperationException($"{nameof(FineGrainedAuthorizationHandler)} was unable to resolve the current endpoint. This handler is only compatible with endpoint routing.");

        var bypass = endpoint.Metadata.GetMetadata<FgaBypass>();
        if (bypass is not null)
        {
            _logger.BypassFound(httpContext.Request.Path);
            context.Succeed(requirement);
            return;
        }
        
        var attributes = endpoint.Metadata.GetOrderedMetadata<FgaAttribute>();
        
        // The user is enforcing the fga policy but there's no attributes here.
        if (attributes.Count == 0)
            return;
        
        var checks = new List<ClientCheckRequest>();
            
        foreach (var attribute in attributes)
        {
            string? user;
            string? relation;
            string? @object;
            List<ClientTupleKey>? contextualTuples;
            try
            {
                user = await attribute.GetUser(httpContext);
                relation = await attribute.GetRelation(httpContext);
                @object = await attribute.GetObject(httpContext);
                contextualTuples = await attribute.GetContextualTuples(httpContext);
            }
            catch (FgaMiddlewareException ex)
            {
                _logger.MiddlewareException(ex);
                return;
            }

            // If we get back nulls from anything we cannot perform a check.
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(relation) || string.IsNullOrEmpty(@object))
            {
                _logger.NullValuesReturned(user, relation, @object);
                return;
            }

            if (!Validation.IsValidObjectReference(user))
            {
                _logger.InvalidUser(user);
                return;
            }
                
            checks.Add(new ClientCheckRequest
            {
                User = user,
                Relation = relation,
                Object = @object,
                ContextualTuples = contextualTuples
            });
        }
          
        var results = await _client.BatchCheck(checks, httpContext.RequestAborted);

        var failedChecks = results.Responses.Where(x=> x.Allowed is false).ToArray();
            
        // log all of reasons for the failed checks
        if (failedChecks.Length > 0)
        {
            foreach (var response in failedChecks)
            {
                if (response.Error is not null)
                {
                    _logger.CheckException(response.Request.User, response.Request.Relation, response.Request.Object, response.Error);
                }
                else if (response.Allowed is false)
                {
                    _logger.CheckFailure(response.Request.User, response.Request.Relation, response.Request.Object);
                }
            }
        }
        else
        {
            context.Succeed(requirement);
        }
    }
}