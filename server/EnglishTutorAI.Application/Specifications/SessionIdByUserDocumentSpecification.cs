using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class SessionIdByUserDocumentSpecification : DataTransformSpecification<UserDocument, Guid>
{
    public SessionIdByUserDocumentSpecification(Guid userDocumentId) : base(
        ud => ud.SessionId,
        ud => ud.Id == userDocumentId)
    {
    }
}