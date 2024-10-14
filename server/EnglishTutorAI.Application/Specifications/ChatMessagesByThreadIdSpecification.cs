using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Specifications;

public class ChatMessagesByThreadIdSpecification : Specification<ChatMessage>
{
    public ChatMessagesByThreadIdSpecification(string threadId, ConversationRole role)
        : base(c => c.ThreadId == threadId && c.ChatType == ChatType.Dialog && c.ConversationRole == role)
    {
        ApplyOrderBy(c => c.CreatedAt);
    }
}