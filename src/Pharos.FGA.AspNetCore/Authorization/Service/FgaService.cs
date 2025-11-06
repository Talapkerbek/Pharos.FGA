using Microsoft.Extensions.Logging;
using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;
using Pharos.FGA.AspNetCore.Authorization.Exceptions;

namespace Pharos.FGA.AspNetCore.Authorization.Service;

public class FgaService(ILogger<FgaService> logger, OpenFgaClient fgaClient) : IFgaService
{
    public async Task AddRelation(string user, string relation, string @object)
    {
        ValidateObjectReferences(user, @object);

        logger.LogInformation(
            "Adding relation: user={user}, relation={relation}, object={object}",
            user, relation, @object
        );

        var tuple = new ClientTupleKey { Object = @object, Relation = relation, User = user };

        var response = await fgaClient.Write(new ClientWriteRequest([tuple]));
        
        foreach (var writeRes in response.Writes)
        {
            if (writeRes.Status == ClientWriteStatus.FAILURE)
            {
                logger.LogError(
                    "Error while adding relation: user={user}, relation={relation}, object={object}, error={error}", user, relation, @object, writeRes.Error
                );
            
                throw new AuthorizationException($"Error while adding relation for user.");
            }
        }
    }

    public async Task DeleteRelation(string user, string relation, string @object)
    {
        ValidateObjectReferences(user, @object);

        logger.LogInformation(
            "Deleting relation: user={user}, relation={relation}, object={object}",
            user, relation, @object
        );

        var tuple = new ClientTupleKeyWithoutCondition { Object = @object, Relation = relation, User = user };

        var response = await fgaClient.DeleteTuples([tuple]);
        foreach (var deleteRes in response.Deletes)
        {
            if (deleteRes.Status == ClientWriteStatus.FAILURE)
            {
                logger.LogError(
                    "Error while deleting relation: user={user}, relation={relation}, object={object}, error={error}",
                    user, relation, @object, deleteRes.Error
                );

                throw new AuthorizationException($"Error while deleting relation for user.");
            }
        }
    }

    public async Task<bool> CheckRelation(string user, string relation, string @object)
    {
        ValidateObjectReferences(user, @object);

        logger.LogInformation(
            "Checking relation: user={user}, relation={relation}, object={object}",
            user, relation, @object
        );
       
        var res = await fgaClient.Check(new ClientCheckRequest
        {
            User = user,
            Relation = relation,
            Object = @object
        });

        logger.LogInformation(
            "Check completed: user={user}, relation={relation}, object={object}, allowed={allowed}",
            user, relation, @object, res.Allowed
        );

        return res.Allowed ?? false;
    }

    private static void ValidateObjectReferences(string user, string @object)
    {
        if (!Validation.IsValidObjectReference(user))
            throw new ArgumentException($"{user} is not a valid object reference.", nameof(user));

        if (!Validation.IsValidObjectReference(@object))
            throw new ArgumentException($"{@object} is not a valid object reference.", nameof(@object));
    }
}