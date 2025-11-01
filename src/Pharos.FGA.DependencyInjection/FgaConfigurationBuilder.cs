using OpenFga.Sdk.Client;
using OpenFga.Sdk.Client.Model;
using OpenFga.Sdk.Configuration;
using Pharos.FGA.DependencyInjection.Configuration;

namespace Pharos.FGA.DependencyInjection;

public sealed class FgaConfigurationBuilder
{
    private FgaConnectionConfiguration? _fgaConfiguration;

    private string? _storeId;

    /// <inheritdoc cref="ClientConfiguration.StoreId"/> 
    public FgaConfigurationBuilder SetStoreId(string storeId)
    {
        _storeId = storeId;
        return this;
    }
    
    private string? _authorizationModelId;

    /// <inheritdoc cref="ClientConfiguration.AuthorizationModelId" />
    public FgaConfigurationBuilder SetAuthorizationModelId(string authorizationModelId)
    {
        _authorizationModelId = authorizationModelId;
        return this;
    }
    
    private int? _maxRetry;

    /// <inheritdoc cref="RetryParams.MaxRetry"/>
    public FgaConfigurationBuilder SetMaxRetry(int maxRetry)
    {
        _maxRetry = maxRetry;
        return this;
    }
    
    private int? _minWaitInMs;
    /// <inheritdoc cref="RetryParams.MinWaitInMs"/>
    public FgaConfigurationBuilder SetWaitInMs(int waitInMs)
    {
        _minWaitInMs = waitInMs;
        return this;
    }
    
    private TelemetryConfig? _telemetryConfig;
    
    /// <inheritdoc cref="TelemetryConfig"/>
    public FgaConfigurationBuilder SetTelemetry(TelemetryConfig config)
    {
        _telemetryConfig = config;
        return this;
    }
    
    /// <summary>
    /// Configures the client for use with OpenFga
    /// </summary>
    /// <param name="config"></param>
    public void ConfigureOpenFga(Action<OpenFgaConnectionBuilder> config)
    {
        ArgumentNullException.ThrowIfNull(config);
        
        var configuration = new OpenFgaConnectionBuilder();
        config.Invoke(configuration);
        _fgaConfiguration = configuration.Build();
    }
    
    internal FgaBuiltConfiguration Build()
    {
        if (_fgaConfiguration is null)
            throw new InvalidOperationException("OpenFga configuration must be set");
        if (string.IsNullOrEmpty(_storeId))
            throw new InvalidOperationException("Store ID must be set");

        return new FgaBuiltConfiguration(_storeId, _authorizationModelId, _maxRetry, _minWaitInMs, _telemetryConfig, _fgaConfiguration);
    }
}