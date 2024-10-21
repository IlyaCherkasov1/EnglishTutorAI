using EnglishTutorAI.Domain.Enums;
using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class LinguaFixMessage : Entity, IHasCreatedAt
{
    public required ConversationRole ConversationRole { get; init; }

    public required string Content { get; init; }

    public DateTime CreatedAt { get; set; }

    public required string ThreadId { get; init; }

    public required Guid DocumentId { get; set; }
}