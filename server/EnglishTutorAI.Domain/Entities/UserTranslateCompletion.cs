using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class UserTranslateCompletion : Entity, IHasCreatedAt
{
    public Guid UserTranslateId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}