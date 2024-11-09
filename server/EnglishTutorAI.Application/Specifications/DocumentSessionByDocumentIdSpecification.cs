using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class DocumentSessionByDocumentIdSpecification : DataTransformSpecification<DocumentSession, Guid>
{
    public DocumentSessionByDocumentIdSpecification(Guid documentId)
        : base(
            s => s.Id,
            s => s.DocumentId == documentId)
    {
    }
}