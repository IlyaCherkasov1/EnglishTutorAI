using EnglishTutorAI.Api.Interfaces;
using EnglishTutorAI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Api.ServiceInstallers;

public class DbInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationDbContext)));
        });
    }
}