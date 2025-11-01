using OpenFga.Sdk.Configuration;

namespace Pharos.FGA.DependencyInjection;

internal sealed record FgaConnectionConfiguration(string ApiUrl);

internal sealed record FgaBuiltConfiguration
(
    string StoreId, 
    string? AuthorizationModelId, 
    int? MaxRetry,
    int? MinWaitInMs,
    TelemetryConfig? TelemetryConfig,
    FgaConnectionConfiguration Connection
);