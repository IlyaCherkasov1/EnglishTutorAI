using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class TranslateRetrievalByIdSpecification : Specification<Translate>
{
    public TranslateRetrievalByIdSpecification(Guid documentId) : base(d => d.Id == documentId)
    {
        AddInclude(d => d.Sentences);
    }
}