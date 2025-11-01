using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;

namespace Pharos.FGA.AspNetCore.Authorization;

/// <summary>
/// Temporary wrapper to allow for easier testing of middleware. Don't take a dependency on this.
/// </summary>
public class FgaCheckDecorator : IFgaCheckDecorator
{
    private readonly OpenFgaClient _fgaClient;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="auth0FgaApi"></param>
    public FgaCheckDecorator(OpenFgaClient auth0FgaApi)
    {
        _fgaClient = auth0FgaApi;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public virtual Task<ClientBatchCheckClientResponse> BatchCheck(List<ClientCheckRequest> request, CancellationToken ct) => _fgaClient.BatchCheck(request, cancellationToken: ct);
}

/// <summary>
/// Temporary wrapper to allow for easier testing of middleware. Don't take a dependency on this.
/// </summary>
public interface IFgaCheckDecorator
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<ClientBatchCheckClientResponse> BatchCheck(List<ClientCheckRequest> request, CancellationToken ct);
}