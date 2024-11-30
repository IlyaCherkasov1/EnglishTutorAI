using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetDocuments;

public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, SearchResult<DocumentListItem>>
{
    private readonly IDocumentSearchService _documentSearchService;

    public GetDocumentsQueryHandler(IDocumentSearchService documentSearchService)
    {
        _documentSearchService = documentSearchService;
    }

    public Task<SearchResult<DocumentListItem>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
    {
        return _documentSearchService.Search(request.Model);
    }
}