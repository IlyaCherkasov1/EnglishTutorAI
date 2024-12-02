using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetNextDocument;

public class GetNextDocumentQueryHandler : IRequestHandler<GetNextDocumentQuery, DocumentListItem?>
{
    private readonly IDocumentSearchService _documentSearchService;

    public GetNextDocumentQueryHandler(IDocumentSearchService documentSearchService)
    {
        _documentSearchService = documentSearchService;
    }

    public async Task<DocumentListItem?> Handle(GetNextDocumentQuery request, CancellationToken cancellationToken)
    {
        return await _documentSearchService.GetNextDocument(request.Model);
    }
}