using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class AdminUserSeeder : IAdminUserSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly IOptions<AdminUserOptions> _adminUserOptions;

    public AdminUserSeeder(UserManager<User> userManager, IOptions<AdminUserOptions> adminUserOptions)
    {
        _userManager = userManager;
        _adminUserOptions = adminUserOptions;
    }

    public async Task SeedAdminUserAsync()
    {
        var email = _adminUserOptions.Value.Email;
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            var newUser = new User
            {
                Id = Guid.Parse("4931e704-6fba-419f-921c-a39840ceee3a"),
                Email = email,
                UserName = email,
                FirstName = _adminUserOptions.Value.FirstName,
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(newUser, _adminUserOptions.Value.Password);
        }
    }
}