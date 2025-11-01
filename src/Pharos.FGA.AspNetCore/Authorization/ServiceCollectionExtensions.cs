using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Pharos.FGA.AspNetCore.Authorization;

/// <summary>
/// Extensions for registering Fga.Net.AspNetCore features to an ASP.NET Core environment.
/// </summary>
public static class ServiceCollectionExtensions
{

    /// <summary>
    /// Adds and configures an <see cref="FineGrainedAuthorizationHandler"/>
    /// </summary>
    /// <param name="collection">The service collection</param>
    /// <param name="middlewareConfig">The delegate for the <see cref="FgaAspNetCoreConfiguration"/> that will be used to configure the underlying middleware</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddOpenFgaMiddleware(this IServiceCollection collection, Action<FgaAspNetCoreConfiguration>? middlewareConfig = null)
    {
        if (middlewareConfig != null)
            collection.Configure(middlewareConfig);
        collection.AddScoped<IFgaCheckDecorator, FgaCheckDecorator>();
        collection.AddScoped<IAuthorizationHandler, FineGrainedAuthorizationHandler>();
        return collection;
    }

    /// <summary>
    /// Adds a <see cref="FineGrainedAuthorizationRequirement"/> to the given policy.
    /// </summary>
    /// <param name="builder">The Authorization Policy Builder to configure</param>
    /// <returns>The authorization policy builder that is being configured</returns>
    public static AuthorizationPolicyBuilder AddFgaRequirement(this AuthorizationPolicyBuilder builder)
    {
        return builder.AddRequirements(new FineGrainedAuthorizationRequirement());
    }
}