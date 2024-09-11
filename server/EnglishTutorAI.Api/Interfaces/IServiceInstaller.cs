namespace EnglishTutorAI.Api.Interfaces;

public interface IServiceInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration);
}