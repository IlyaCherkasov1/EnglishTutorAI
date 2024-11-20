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
    private readonly IUnitOfWork _unitOfWork;

    public AdminUserSeeder(
        UserManager<User> userManager,
        IOptions<AdminUserOptions> adminUserOptions,
        IRepository<UserStatistics> userStatisticsRepository,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _adminUserOptions = adminUserOptions;
        _userStatisticsRepository = userStatisticsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedAdminUserAsync()
    {
        var user = await _userManager.FindByIdAsync(UserRoleIds.AdminUser);

        if (user == null)
        {
            using var transaction = _unitOfWork.BeginTransaction();
            var email = _adminUserOptions.Value.Email;

            var newUser = new User
            {
                Id = Guid.Parse(UserRoleIds.AdminUser),
                Email = email,
                UserName = email,
                FirstName = _adminUserOptions.Value.FirstName,
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(newUser, _adminUserOptions.Value.Password);
            await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
            await _userStatisticsRepository.Add(new UserStatistics
            {
                UserId = newUser.Id,
                CorrectedMistakes = 0,
            });

            transaction.Commit();
        }
    }
}