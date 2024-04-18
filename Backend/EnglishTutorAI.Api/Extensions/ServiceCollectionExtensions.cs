using EnglishTutorAI.Application.Configurations;

namespace EnglishTutorAI.Api.Extensions;

internal static class ServiceCollectionExtensions
{
public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OpenAiConfig>(configuration.GetSection(nameof(OpenAiConfig)));
        services.Configure<ProxyConfig>(configuration.GetSection(nameof(ProxyConfig)));

        return services;
    }
}