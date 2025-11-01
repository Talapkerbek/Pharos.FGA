namespace Pharos.FGA.DependencyInjection.Configuration;

public sealed class OpenFgaConnectionBuilder
{
    private string? _apiUrl;

    public OpenFgaConnectionBuilder SetConnection(string apiUrl)
    {
        _apiUrl = apiUrl;
        return this;
    }

    internal FgaConnectionConfiguration Build()
    {
        if (string.IsNullOrEmpty(_apiUrl))
            throw new InvalidOperationException("API Host cannot be null or empty");
        if (!_apiUrl.Contains(Uri.UriSchemeHttp) && !_apiUrl.Contains(Uri.UriSchemeHttps))
            throw new InvalidOperationException("API Scheme must be Http or Https");
        
        return new FgaConnectionConfiguration(_apiUrl);
    }
}