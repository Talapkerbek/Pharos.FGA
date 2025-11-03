using Microsoft.AspNetCore.Http;
using Pharos.FGA.AspNetCore.Authorization.Exceptions;

namespace Pharos.FGA.AspNetCore.Authorization.Attributes;

/// <summary>
/// Computes a FGA Authorization check based on the predefined objectId
/// </summary>
public class FgaStringObjectAttribute : FgaBaseObjectAttribute
{
    private readonly string _relation;
    private readonly string _type;
    private readonly string _object;

    /// <summary>
    /// Computes a FGA Authorization check based on the header value of the request
    /// </summary>
    /// <param name="relation">The relationship to check, such as writer or viewer</param>
    /// <param name="type">The relation between the user and object</param>
    /// <param name="object">The object with type and id in 'objectType:id' format. Will throw an exception if not present.</param>
    public FgaStringObjectAttribute(string relation, string type, string @object)
    {
        _relation = relation;
        _type = type;
        _object = @object;
    }

    /// <inheritdoc />
    public override ValueTask<string> GetRelation(HttpContext context)
    {
        return ValueTask.FromResult(_relation);
    }

    /// <inheritdoc />
    public override ValueTask<string> GetObject(HttpContext context)
    {
        if (Validation.IsValidObjectReference(_object))
        {
            return ValueTask.FromResult(_object);
        }

        throw new FgaMiddlewareException($"Header {_object} was not present in the request");
    }
}