using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Specifications;

public class ChatMessagesByThreadIdSpecification : Specification<ChatMessage>
{
    public ChatMessagesByThreadIdSpecification(string threadId, ChatType chatType)
        : base(c => c.ThreadId == threadId && c.ChatType == chatType)
    {
        ApplyOrderBy(c => c.CreatedAt);
    }
}