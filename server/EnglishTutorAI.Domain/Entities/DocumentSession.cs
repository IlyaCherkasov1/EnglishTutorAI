using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class DocumentSession : Entity, IHasCreatedAt
{
    public Guid DocumentId { get; set; }

    public DateTime CreatedAt { get; set; }
}