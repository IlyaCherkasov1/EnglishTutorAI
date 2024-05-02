using EnglishTutorAI.Api.Interfaces;

namespace EnglishTutorAI.Api.ServiceInstallers;

public class SwaggerInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen();
    }
}