using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Models;

namespace EnglishTutorAI.Api.Extensions;

internal static class ServiceCollectionExtensions
{
public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OpenAiConfig>(configuration.GetSection(nameof(OpenAiConfig)));
        services.Configure<ProxyConfig>(configuration.GetSection(nameof(ProxyConfig)));
        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
        services.Configure<AwsSettings>(configuration.GetSection("AWS"));
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.Configure<RefreshTokenSettings>(configuration.GetSection(nameof(RefreshTokenSettings)));
        services.Configure<GoogleKeys>(configuration.GetSection(nameof(GoogleKeys)));
        services.Configure<FacebookKeys>(configuration.GetSection(nameof(FacebookKeys)));

        return services;
    }
}