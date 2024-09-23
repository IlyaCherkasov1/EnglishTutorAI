using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class ValidUserSessionByUserIdSpecification : Specification<UserSession>
{
    public ValidUserSessionByUserIdSpecification(Guid userId) : base(
        s => s.User.Id == userId && s.IsValid && DateTime.UtcNow < s.Expires)
    {
        AddInclude(s => s.User);
    }
}