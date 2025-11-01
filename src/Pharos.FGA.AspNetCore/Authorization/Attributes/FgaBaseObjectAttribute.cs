using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Pharos.FGA.AspNetCore.Authorization.Attributes;

public abstract class FgaBaseObjectAttribute : FgaAttribute
{
    /// <inheritdoc cref="FgaAttribute.GetUser" />
    public override ValueTask<string> GetUser(HttpContext context)
    {
        var config = context.RequestServices.GetRequiredService<IOptions<FgaAspNetCoreConfiguration>>().Value;
        return ValueTask.FromResult(config.UserIdentityResolver!(context.User));
    }
}