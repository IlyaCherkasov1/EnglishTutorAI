using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models;

public class ChatMessageResponse
{
    public Guid Id { get; set; }

    public ConversationRole ConversationRole { get; set; }

    public required string Content { get; set; }
}