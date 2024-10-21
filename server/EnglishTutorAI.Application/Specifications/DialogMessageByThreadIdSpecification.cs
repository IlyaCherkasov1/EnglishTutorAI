using EnglishTutorAI.Application.Specifications.Configurations;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications;

public class DialogMessageByThreadIdSpecification : Specification<DialogMessage>
{
    public DialogMessageByThreadIdSpecification(string threadId)
        : base(c => c.ThreadId == threadId)
    {
    }
}