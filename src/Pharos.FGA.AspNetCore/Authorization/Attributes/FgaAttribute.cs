using OpenFga.Sdk.Client.Model;
using Microsoft.AspNetCore.Http;

namespace Pharos.FGA.AspNetCore.Authorization.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public abstract class FgaAttribute : Attribute
{
     /// <summary>
    /// An entity in the system that can be related to an object.
    /// </summary>
    /// <param name="context">The context of the current request</param>
    /// <returns>A user identifier</returns>
    public abstract ValueTask<string> GetUser(HttpContext context);
    /// <summary>
    /// Defines the possibility of a relationship between objects of this type and other users in the system.
    /// </summary>
    /// <param name="context">The context of the current request</param>
    /// <returns>A relationship, such as reader or writer</returns>
    public abstract ValueTask<string> GetRelation(HttpContext context);
    /// <summary>
    /// An entity in the system. 
    /// </summary>
    /// <param name="context">The context of the current request</param>
    /// <returns>Usually a string in an entity-identifier format: document:id</returns>
    public abstract ValueTask<string> GetObject(HttpContext context);
    
    /// <summary>
    /// Contextual tuple(s) to apply the check generated from this attribute.
    /// </summary>
    /// <param name="context">The context of the current request</param>
    /// <returns>The list of contextual tuples, or null if none were provided</returns>
    public virtual ValueTask<List<ClientTupleKey>?> GetContextualTuples(HttpContext context) => new((List<ClientTupleKey>?)null);

    /// <summary>
    /// Concats the type and identifier into the object format
    /// </summary>
    /// <param name="type">The objects type, such as workspace, repository, organization or document</param>
    /// <param name="identifier">The objects identifier</param>
    /// <returns>The object in the entity:identifier format</returns>
    protected static string FormatObject(string type, string identifier) => $"{type}:{identifier}";
}