using EnglishTutorAI.Api.Extensions;
using EnglishTutorAI.Api.Interfaces;
using EnglishTutorAI.Api.Services.Formatters;
using EnglishTutorAI.Application;

namespace EnglishTutorAI.Api.ServiceInstallers;

internal class CommonInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddApplicationDependencies();

        services.AddHsts(options =>
        {
            options.IncludeSubDomains = true;
            options.MaxAge = TimeSpan.FromDays(730);
            options.Preload = true;
        });

        services.AddControllers(options => options.InputFormatters.Insert(0, new TrimStringInputFormatter()));
        services.AddSettings(configuration);
    }
}