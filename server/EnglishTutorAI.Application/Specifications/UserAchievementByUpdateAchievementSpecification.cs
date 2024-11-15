using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class UserAchievementByUpdateAchievementSpecification : Specification<UserAchievement>
{
    public UserAchievementByUpdateAchievementSpecification(Guid userId, Guid achievementId) :
        base(ua => ua.UserId == userId && ua.AchievementId == achievementId)
    {
        AddInclude(ua => ua.Achievement);
        AddInclude(ua => ua.Achievement.AchievementLevels);
    }
}