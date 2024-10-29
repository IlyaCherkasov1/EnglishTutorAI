using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishTutorAI.Infrastructure.SeedConfiguration;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.HasData(new IdentityUserRole<Guid>
        {
            UserId = Guid.Parse("4931e704-6fba-419f-921c-a39840ceee3a"),
            RoleId = Guid.Parse("9E2C2F8B-410E-422F-A472-DAE5BF8F7EE7"),
        });
    }
}