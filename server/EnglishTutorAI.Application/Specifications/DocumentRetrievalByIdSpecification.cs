using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class DocumentRetrievalByIdSpecification : Specification<Document>
{
    public DocumentRetrievalByIdSpecification(Guid documentId) : base(d => d.Id == documentId)
    {
        AddInclude(d => d.Sentences);
    }
}