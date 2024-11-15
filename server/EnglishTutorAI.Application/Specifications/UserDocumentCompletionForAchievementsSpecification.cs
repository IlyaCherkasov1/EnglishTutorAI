using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class UserDocumentCompletionForAchievementsSpecification : Specification<UserDocumentCompletion>
{
    public UserDocumentCompletionForAchievementsSpecification(Guid userId, Guid documentId) : base(
        ud => ud.UserId == userId && ud.DocumentId == documentId)
    {
    }
}