using Microsoft.Extensions.DependencyInjection;

namespace EnglishTutorAI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));

        return services;
    }
}