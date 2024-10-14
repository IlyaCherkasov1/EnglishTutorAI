using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Models;

public class ChatMessageResponse
{
    public ConversationRole ConversationRole { get; set; }

    public required string Content { get; set; }
}