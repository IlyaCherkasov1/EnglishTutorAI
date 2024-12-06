using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class UserDocumentRetrievalByIdSpecification : Specification<UserDocument>
{
    public UserDocumentRetrievalByIdSpecification(Guid documentId, Guid userId) : base(
        ud => ud.DocumentId == documentId && ud.UserId == userId)
    {
        AddInclude(d => d.Document);
        AddInclude(d => d.Document.Sentences);
    }
}