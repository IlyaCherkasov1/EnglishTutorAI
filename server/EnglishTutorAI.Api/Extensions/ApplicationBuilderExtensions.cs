using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task ApplyMigrationsAndSeedAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
        await scope.ServiceProvider.GetRequiredService<IAdminUserSeeder>().SeedAdminUserAsync();
    }
}