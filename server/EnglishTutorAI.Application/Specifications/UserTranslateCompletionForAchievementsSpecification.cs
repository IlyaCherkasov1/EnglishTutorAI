using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class UserTranslateCompletionForAchievementsSpecification : Specification<UserTranslateCompletion>
{
    public UserTranslateCompletionForAchievementsSpecification(Guid userId, Guid userTranslateId) : base(
        ud => ud.UserId == userId && ud.UserTranslateId == userTranslateId)
    {
    }
}