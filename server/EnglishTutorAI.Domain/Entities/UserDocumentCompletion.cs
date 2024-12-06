using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class UserDocumentCompletion : Entity, IHasCreatedAt
{
    public Guid UserDocumentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}