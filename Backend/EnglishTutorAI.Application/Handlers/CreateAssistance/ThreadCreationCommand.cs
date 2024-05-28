using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.CreateAssistance;

public class ThreadCreationCommand : IRequest<ThreadCreationResponse>
{
    public ThreadCreationCommand(Guid documentId)
    {
        DocumentId = documentId;
    }

    public Guid DocumentId { get; }
}