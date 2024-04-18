using EnglishTutorAI.Api.Interfaces;
using EnglishTutorAI.Application.Profiles;

namespace EnglishTutorAI.Api.ServiceInstallers;

public class MapperInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(ApplicationMappingProfile));
    }
}