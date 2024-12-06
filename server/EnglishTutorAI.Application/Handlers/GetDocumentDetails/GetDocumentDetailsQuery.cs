using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocumentDetails;

public class GetDocumentDetailsQuery : IRequest<DocumentDetailsModel>
{
    public GetDocumentDetailsQuery(Guid documentId)
    {
        DocumentId = documentId;
    }

    public Guid DocumentId { get; }
}