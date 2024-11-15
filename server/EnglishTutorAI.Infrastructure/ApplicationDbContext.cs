using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Infrastructure.Data;
using EnglishTutorAI.Infrastructure.SeedConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Document> Documents { get; init; }

    public DbSet<DialogMessage> DialogMessages { get; init; }

    public DbSet<UserSession> UserSessions { get; init; }

    public DbSet<LinguaFixMessage> LinguaFixMessages { get; init; }

    public DbSet<DocumentSession> DocumentSessions { get; init; }

    public DbSet<Achievement> Achievements { get; init; }

    public DbSet<UserAchievement> UserAchievements { get; init; }

    public DbSet<AchievementLevel> AchievementLevels { get; init; }

    public DbSet<UserStatistics> UserStatistics { get; init; }

    public DbSet<UserDocumentCompletion> UserDocumentCompletions { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        InitialDataConfigurator.SetupSystemData(builder);
    }
}