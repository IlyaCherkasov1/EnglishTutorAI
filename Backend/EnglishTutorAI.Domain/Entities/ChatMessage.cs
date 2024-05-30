using EnglishTutorAI.Domain.Enums;
using EnglishTutorAI.Domain.Interfaces;

namespace EnglishTutorAI.Domain.Entities;

public class ChatMessage : Entity, IHasCreatedAt
{
    public ConversationRole ConversationRole { get; set; }

    public required string Content { get; set; }

    public required DateTime CreatedAt { get; set; }

    public ChatType ChatType { get; set; }

    public required string ThreadId { get; set; }
}