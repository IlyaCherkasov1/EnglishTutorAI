using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class SessionIdByUserTranslateSpecification : DataTransformSpecification<UserTranslate, Guid>
{
    public SessionIdByUserTranslateSpecification(Guid userTranslateId) : base(
        ud => ud.SessionId,
        ud => ud.Id == userTranslateId)
    {
    }
}