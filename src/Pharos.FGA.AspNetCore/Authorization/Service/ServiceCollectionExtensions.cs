using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Pharos.FGA.AspNetCore.Authorization.Service;

/// <summary>
/// Extensions for registering Fga.Net.AspNetCore features to an ASP.NET Core environment.
/// </summary>
public static class ServiceCollectionExtensions
{

    /// <summary>
    /// Adds and configures an <see cref="FgaService"/>
    /// </summary>
    /// <param name="collection">The service collection</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddFgaService(this IServiceCollection collection)
    {
        collection.AddScoped<IFgaService, FgaService>();
        return collection;
    }
}