using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnglishTutorAI.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Document> Documents { get; set; }

    public DbSet<DialogMessage> DialogMessages { get; set; }

    public DbSet<UserSession> UserSessions { get; set; }

    public DbSet<LinguaFixMessage> LinguaFixMessages { get; set; }
}