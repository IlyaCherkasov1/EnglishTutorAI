using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnglishTutorAI.Infrastructure.SeedConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role
            {
                Id = Guid.Parse("17B372AE-9678-49F6-BBE6-A90AE8E3C6AB"),
                Name = UserRoles.User,
                NormalizedName = UserRoles.User.ToUpper(),
            },
            new Role
            {
                Id = Guid.Parse("9E2C2F8B-410E-422F-A472-DAE5BF8F7EE7"),
                Name = UserRoles.Admin,
                NormalizedName = UserRoles.Admin.ToUpper(),
            }
        );
    }
}