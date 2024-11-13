using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class DocumentSession : Entity, IHasCreatedAt
{
    public DateTime CreatedAt { get; set; }

    public Guid DocumentId { get; set; }

    public Document Document { get; set; } = null!;
}