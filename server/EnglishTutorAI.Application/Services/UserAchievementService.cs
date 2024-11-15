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
    private readonly IAuthenticatedUserContext _authenticatedUserContext;
    private const int ProgressStep = 1;

    public UserAchievementService(
        IRepository<UserAchievement> userAchievementRepository,
        IAuthenticatedUserContext authenticatedUserContext)
    {
        _userAchievementRepository = userAchievementRepository;
        _authenticatedUserContext = authenticatedUserContext;
    }

    public async Task<IEnumerable<UserAchievementResponse>> GetUserAchievements()
    {
        var userId = _authenticatedUserContext.UserId!.Value;

        return await _userAchievementRepository.List(new UserAchievementByUserIdSpecification(userId));
    }

    public async Task UpdateProgress(Guid userId, Guid achievementId)
    {
        var userAchievement = await _userAchievementRepository.Single(
            new UserAchievementByUpdateAchievementSpecification(userId, achievementId));

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
                userAchievement.Achievement.IsCompleted = true;
            }
        }
    }
}