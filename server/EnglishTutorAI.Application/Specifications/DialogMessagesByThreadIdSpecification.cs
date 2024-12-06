using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;

namespace EnglishTutorAI.Application.Specifications;

public class DialogMessagesByThreadIdSpecification : Specification<DialogMessage>
{
    public DialogMessagesByThreadIdSpecification(string threadId, ConversationRole role)
        : base(c => c.UserDocument.ThreadId == threadId && c.ConversationRole == role)
    {
        ApplyOrderBy(c => c.CreatedAt);
    }
}