using System.Net;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Infrastructure.HttpClients;

[SingletonDependency]
public class ConfigurableProxyHttpClientFactory : IHttpClientFactory
{
    private readonly ProxyConfig _proxyConfig;

    public ConfigurableProxyHttpClientFactory(IOptionsMonitor<ProxyConfig> proxyOptions)
    {
        _proxyConfig = proxyOptions.CurrentValue;
    }

    public HttpClient CreateClient(string name)
    {
        var proxy = new WebProxy(_proxyConfig.Url)
        {
            Credentials = new NetworkCredential(_proxyConfig.Username, _proxyConfig.Password)
        };
        var httpClientHandler = new HttpClientHandler { Proxy = proxy };

        return new HttpClient(httpClientHandler);
    }
}