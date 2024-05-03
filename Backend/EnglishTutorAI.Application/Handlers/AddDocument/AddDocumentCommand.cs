using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.AddDocument;

public class AddDocumentCommand : IRequest
{
    public DocumentCreationRequest CreationRequest { get; }

    public AddDocumentCommand(DocumentCreationRequest creationRequest)
    {
        CreationRequest = creationRequest;
    }
}