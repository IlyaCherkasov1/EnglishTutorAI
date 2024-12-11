using EnglishTutorAI.Domain.Enums;
using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class DialogMessage : Entity, IHasCreatedAt
{
    public required ConversationRole ConversationRole { get; init; }

    public required string Content { get; init; }

    public DateTime CreatedAt { get; set; }

    public UserTranslate UserTranslate { get; init; } = null!;

    public required Guid UserTranslateId { get; init; }
}