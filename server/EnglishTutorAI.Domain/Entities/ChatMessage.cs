using EnglishTutorAI.Domain.Enums;
using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class ChatMessage : Entity, IHasCreatedAt
{
    public ConversationRole ConversationRole { get; init; }

    public required string Content { get; init; }

    public DateTime CreatedAt { get; set; }

    public ChatType ChatType { get; init; }

    public required string ThreadId { get; init; }
}