using System.Security.Claims;

namespace Pharos.FGA.AspNetCore;

public sealed class FgaAspNetCoreConfiguration
{
    internal Func<ClaimsPrincipal, string>? UserIdentityResolver { get; private set; }

    public void SetUserIdentityResolver(string type, Func<ClaimsPrincipal, string> idResolver)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(idResolver);
        
        UserIdentityResolver = principal => $"{type}:{idResolver(principal)}";
    }
}