using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class UserDocumentIsCompletedSpecification : DataTransformSpecification<UserDocument, bool>
{
    public UserDocumentIsCompletedSpecification(Guid userDocumentId) : base(
         ud => ud.Id == userDocumentId, ud => ud.IsCompleted)
    {
    }
}