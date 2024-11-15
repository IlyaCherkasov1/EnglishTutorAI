using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class UserManagementService : IUserManagementService
{
    private readonly IRepository<UserStatistics> _userStatisticsRepository;
    private readonly IUserAchievementService _userAchievementService;

    public UserManagementService(
        IRepository<UserStatistics> userStatisticsRepository,
        IUserAchievementService userAchievementService)
    {
        _userStatisticsRepository = userStatisticsRepository;
        _userAchievementService = userAchievementService;
    }

    public async Task UpdateStatistics(Guid userId, int mistakeCount)
    {
        var userStatistics = await _userStatisticsRepository.Single(new UserStatisticsByUserIdSpecification(userId));
        userStatistics.CorrectedMistakes += mistakeCount;
    }

    public async Task UpdateAchievement(Guid userId, Guid achievementId) =>
        await _userAchievementService.UpdateProgress(userId, achievementId);
}