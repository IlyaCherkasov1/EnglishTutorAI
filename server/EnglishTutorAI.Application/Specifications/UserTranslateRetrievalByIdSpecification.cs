using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class UserTranslateRetrievalByIdSpecification : Specification<UserTranslate>
{
    public UserTranslateRetrievalByIdSpecification(Guid translateId, Guid userId) : base(
        ud => ud.TranslateId == translateId && ud.UserId == userId)
    {
        AddInclude(d => d.Translate);
        AddInclude(d => d.Translate.Sentences);
    }
}