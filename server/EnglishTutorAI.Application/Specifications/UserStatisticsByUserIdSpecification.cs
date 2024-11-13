using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class UserStatisticsByUserIdSpecification : Specification<UserStatistics>
{
    public UserStatisticsByUserIdSpecification(Guid userId) : base(us => us.UserId == userId)
    {
    }
}