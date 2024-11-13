using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Constants;
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
    private readonly IRepository<UserStatistics> _userStatisticsRepository;

    public AdminUserSeeder(
        UserManager<User> userManager,
        IOptions<AdminUserOptions> adminUserOptions,
        IRepository<UserStatistics> userStatisticsRepository)
    {
        _userManager = userManager;
        _adminUserOptions = adminUserOptions;
        _userStatisticsRepository = userStatisticsRepository;
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
            await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
            await _userStatisticsRepository.Add(new UserStatistics
            {
                UserId = user.Id,
                CorrectedMistakes = 0,
            });
        }
    }
}