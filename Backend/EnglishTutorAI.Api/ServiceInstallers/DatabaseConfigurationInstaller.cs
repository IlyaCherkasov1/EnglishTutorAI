using EnglishTutorAI.Api.Interfaces;
using EnglishTutorAI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Api.ServiceInstallers;

public class DatabaseConfigurationInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString(nameof(ApplicationDbContext)), builder =>
                    builder.CommandTimeout(configuration.GetValue<int>("DbConnectionTimeout")));
        });
    }
}