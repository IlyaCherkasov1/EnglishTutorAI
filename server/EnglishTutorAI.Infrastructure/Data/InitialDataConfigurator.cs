using EnglishTutorAI.Infrastructure.SeedConfiguration;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Infrastructure.Data;

public static class InitialDataConfigurator
{
    public static void SetupSystemData(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new AchievementConfiguration());
        builder.ApplyConfiguration(new AchievementLevelConfiguration());
    }
}