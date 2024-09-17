using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class RefreshTokenByValueSpecification : Specification<RefreshToken>
{
    public RefreshTokenByValueSpecification(string token) : base(t => t.Token == token)
    {
        AddInclude(t => t.User);
    }
}