using EnglishTutorAI.Api.Interfaces;
using EnglishTutorAI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Api.ServiceInstallers;

public class DatabaseEntriesInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        scope.ServiceProvider.GetService<ApplicationDbContext>()!.Database.Migrate();
    }
}