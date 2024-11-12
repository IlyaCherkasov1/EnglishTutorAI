using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class UserAchievementExistsByUserIdSpecification : Specification<UserAchievement>
{
    public UserAchievementExistsByUserIdSpecification(Guid userId) : base(u => u.UserId == userId)
    {
    }
}