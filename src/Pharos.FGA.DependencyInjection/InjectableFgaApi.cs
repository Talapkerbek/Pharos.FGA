using Microsoft.Extensions.Options;
using OpenFga.Sdk.Api;
using OpenFga.Sdk.Client;
using Pharos.FGA.DependencyInjection.Configuration;

namespace Pharos.FGA.DependencyInjection;

internal sealed class InjectableFgaApi : OpenFgaApi
{
    public InjectableFgaApi(IOptions<FgaClientConfiguration> configuration, IHttpClientFactory httpClientFactory) : base(configuration.Value.PurgeHeader(), httpClientFactory.CreateClient(Constants.FgaHttpClient))
    {
    }
}


internal sealed class InjectableFgaClient : OpenFgaClient
{
    public InjectableFgaClient(IOptions<FgaClientConfiguration> configuration, IHttpClientFactory httpClientFactory) : base(configuration.Value.PurgeHeader(), httpClientFactory.CreateClient(Constants.FgaHttpClient))
    {
    }
}

internal static class ConfigurationExtensions
{
    // This fixes an exception when using API Tokens and both clients.
    public static FgaClientConfiguration PurgeHeader(this FgaClientConfiguration configuration)
    {
        configuration.DefaultHeaders.Remove("Authorization");
        return configuration;
    }
}