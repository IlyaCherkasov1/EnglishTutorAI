using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class DocumentRetrievalByIdSpecification : Specification<Document>
{
    public DocumentRetrievalByIdSpecification(Guid id) : base(d => d.Id == id)
    {
        AddInclude(d => d.Sentences);
    }
}