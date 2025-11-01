using Microsoft.AspNetCore.Http;

namespace Pharos.FGA.AspNetCore.Authorization.Attributes;

/// <summary>
/// A simple implementation of <see cref="FgaAttribute"/> that accepts all values as hardcoded strings.
/// Useful for debugging and testing purposes. Do not use in a real application
/// </summary>
public sealed class FgaStringAttribute : FgaAttribute
{
    private readonly string _user;
    private readonly string _relation;
    private readonly string _object;
    /// <summary>
    /// Constructs a new <see cref="FgaStringAttribute"/>
    /// </summary>
    /// <param name="user"></param>
    /// <param name="relation"></param>
    /// <param name="object"></param>
    public FgaStringAttribute(string user, string relation, string @object)
    {
        _user = user;
        _relation = relation;
        _object = @object;
    }
    /// <inheritdoc />
    public override ValueTask<string> GetUser(HttpContext context) => ValueTask.FromResult(_user);
    /// <inheritdoc/>
    public override ValueTask<string> GetRelation(HttpContext context) => ValueTask.FromResult(_relation);
    /// <inheritdoc />
    public override ValueTask<string> GetObject(HttpContext context) => ValueTask.FromResult(_object);
}