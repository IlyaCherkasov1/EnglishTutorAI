using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class UserAchievementService : IUserAchievementService
{
    private readonly IRepository<UserAchievement> _userAchievementRepository;
    private readonly IUserContextService _userContextService;
    private const int ProgressStep = 1;

    public UserAchievementService(
        IRepository<UserAchievement> userAchievementRepository, IUserContextService userContextService)
    {
        _userAchievementRepository = userAchievementRepository;
        _userContextService = userContextService;
    }

    public async Task<IEnumerable<UserAchievementResponse>> GetUserAchievements()
    {
        return await _userAchievementRepository.List(
            new UserAchievementByUserIdSpecification(_userContextService.UserId));
    }

    public async Task UpdateProgress(Guid userId, Guid achievementId)
    {
        var userAchievement = await _userAchievementRepository.Single(
            new UserAchievementByUpdateAchievementSpecification(userId, achievementId));

        if (userAchievement.IsCompleted)
        {
            return;
        }

        userAchievement.Progress += ProgressStep;

        var currentLevel = userAchievement.CurrentLevel;
        var levelGoal = userAchievement.Achievement.AchievementLevels
            .OrderBy(al => al.Goal)
            .ElementAt(currentLevel).Goal;

        if (userAchievement.Progress >= levelGoal)
        {
            userAchievement.CurrentLevel++;

            if (userAchievement.CurrentLevel >= userAchievement.Achievement.AchievementLevels.Count)
            {
                userAchievement.IsCompleted = true;
            }
        }
    }
}