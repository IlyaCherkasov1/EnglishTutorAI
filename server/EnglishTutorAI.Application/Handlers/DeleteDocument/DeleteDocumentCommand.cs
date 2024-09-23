using MediatR;

namespace EnglishTutorAI.Application.Handlers.DeleteDocument;

public class DeleteDocumentCommand : IRequest
{
    public DeleteDocumentCommand(Guid documentId)
    {
        DocumentId = documentId;
    }

    public Guid DocumentId { get; }
}