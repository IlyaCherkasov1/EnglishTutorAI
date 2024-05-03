using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocuments;

public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, IReadOnlyList<DocumentListItem>>
{
    private readonly IDocumentRetrievalService _documentRetrievalService;

    public GetDocumentsQueryHandler(IDocumentRetrievalService documentRetrievalService)
    {
        _documentRetrievalService = documentRetrievalService;
    }

    public Task<IReadOnlyList<DocumentListItem>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
    {
        return _documentRetrievalService.GetAllDocuments();
    }
}