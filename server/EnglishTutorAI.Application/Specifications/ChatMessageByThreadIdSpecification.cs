using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class ChatMessageByThreadIdSpecification : Specification<ChatMessage>
{
    public ChatMessageByThreadIdSpecification(string threadId)
        : base(c => c.ThreadId == threadId)
    {
    }
}