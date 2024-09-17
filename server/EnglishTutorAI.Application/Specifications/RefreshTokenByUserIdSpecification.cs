using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class RefreshTokenByUserIdSpecification : Specification<RefreshToken>
{
    public RefreshTokenByUserIdSpecification(Guid userId) : base(r => r.User.Id == userId)
    {
        AddInclude(t => t.User);
    }
}