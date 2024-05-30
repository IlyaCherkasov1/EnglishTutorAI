using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Specifications;

public class ChatMessagesByThreadIdSpecification : Specification<ChatMessage>
{
    public ChatMessagesByThreadIdSpecification(string threadId)
        : base(c => c.ThreadId == threadId)
    {
        ApplyOrderBy(c => c.CreatedAt);
    }
}