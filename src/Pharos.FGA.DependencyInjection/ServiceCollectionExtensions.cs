using Microsoft.Extensions.DependencyInjection;
using OpenFga.Sdk.Api;
using OpenFga.Sdk.Client;
using Pharos.FGA.DependencyInjection.Configuration;

namespace Pharos.FGA.DependencyInjection;

/// <summary>
/// Extensions for registering Fga features to a <see cref="IServiceCollection"/>
/// </summary>
public static class ServiceCollectionExtensions
{

    /// <summary>
    /// Registers and configures an <see cref="OpenFgaClient"/> and <see cref="OpenFgaApi"/> for the provided service collection.
    /// Both clients are registered as singletons.
    /// </summary>
    /// <param name="collection">The service collection</param>
    /// <param name="fgaConfiguration">The lambda that configures the builder</param>
    /// <returns>An <see cref="IHttpClientBuilder" /> that can be used to further configure the underlying <see cref="HttpClient"/></returns>
    public static IHttpClientBuilder AddOpenFgaClient(this IServiceCollection collection,
        Action<FgaConfigurationBuilder> fgaConfiguration)
    {
        // Add a HTTP factory instance that's suitable for injection into a singleton service.
        var httpClient = collection.AddHttpClient(Constants.FgaHttpClient)
            .ConfigurePrimaryHttpMessageHandler(() =>
                new SocketsHttpHandler()
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(2)
                })
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

        collection.AddSingleton<OpenFgaApi, InjectableFgaApi>();
        collection.AddSingleton<OpenFgaClient, InjectableFgaClient>();

        var configRoot = new FgaConfigurationBuilder();
        fgaConfiguration.Invoke(configRoot);

        var config = configRoot.Build();

        collection.Configure<FgaClientConfiguration>(x =>
            x.ConfigureFgaOptions(config));

        return httpClient;
    }

    /// <summary>
    /// Replaces the existing FGA configuration options with the new configuration. Useful within test infrastructure.
    /// </summary>
    /// <param name="collection">The service collection</param>
    /// <param name="fgaConfiguration">The lambda that configures the builder</param>
    public static void PostConfigureFgaClient(this IServiceCollection collection,
        Action<FgaConfigurationBuilder> fgaConfiguration)
    {
        var configBuilder = new FgaConfigurationBuilder();
        fgaConfiguration.Invoke(configBuilder);

        var config = configBuilder.Build();

        collection.PostConfigure<FgaClientConfiguration>(x =>
            x.ConfigureFgaOptions(config));
    }


    private static void ConfigureFgaOptions(this FgaClientConfiguration x, FgaBuiltConfiguration config)
    {
        x.ApiUrl = config.Connection.ApiUrl;

        x.StoreId = config.StoreId;
        x.AuthorizationModelId = config.AuthorizationModelId;
        if (config.MaxRetry.HasValue)
            x.MaxRetry = config.MaxRetry.Value;
        if (config.MinWaitInMs.HasValue)
            x.MinWaitInMs = config.MinWaitInMs.Value;

        x.Telemetry = config.TelemetryConfig;
    }
}