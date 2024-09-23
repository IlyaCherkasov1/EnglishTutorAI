using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class ValidUserSessionByRefreshTokenSpecification : Specification<UserSession>
{
    public ValidUserSessionByRefreshTokenSpecification(string token)
        : base(s => s.RefreshToken == token && s.IsValid && DateTime.UtcNow < s.Expires)
    {
        AddInclude(s => s.User);
    }
}