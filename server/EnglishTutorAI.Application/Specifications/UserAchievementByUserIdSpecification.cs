using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class UserAchievementByUserIdSpecification : DataTransformSpecification<UserAchievement, UserAchievementResponse>
{
    public UserAchievementByUserIdSpecification(Guid userId) : base(
        ua => new UserAchievementResponse
        {
            AchievementId = ua.Achievement.Id,
            Name = ua.Achievement.Name,
            Description = ua.Achievement.Description,
            LevelGoals = ua.Achievement.AchievementLevels.Select(g => g.Goal).ToList(),
            Progress = ua.Progress,
            CurrentLevel = ua.CurrentLevel,
            IsCompleted = ua.IsCompleted,
            IconFileName = ua.Achievement.IconFileName,
        },
        u => u.UserId == userId)
    {
        AddInclude(a => a.Achievement);
        AddInclude(a => a.Achievement.AchievementLevels);
        AddInclude(a => a.User.UserStatistics);
    }
}