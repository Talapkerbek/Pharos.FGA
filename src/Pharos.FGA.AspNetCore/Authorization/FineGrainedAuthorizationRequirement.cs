using Microsoft.AspNetCore.Authorization;

namespace Pharos.FGA.AspNetCore.Authorization;

internal sealed class FineGrainedAuthorizationRequirement : IAuthorizationRequirement
{
    public override string ToString() =>
        $"{nameof(FineGrainedAuthorizationRequirement)}: Requires FGA Authorization checks to pass.";

}