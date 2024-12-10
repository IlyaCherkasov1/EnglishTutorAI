using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetCompletedDocuments;

public class GetCompletedDocumentsQueryHandler :
    IRequestHandler<GetCompletedDocumentsQuery, SearchResult<CompletedDocumentListItem>>
{
    private readonly IDocumentCompletionService _documentCompletionService;

    public GetCompletedDocumentsQueryHandler(IDocumentCompletionService documentCompletionService)
    {
        _documentCompletionService = documentCompletionService;
    }

    public Task<SearchResult<CompletedDocumentListItem>> Handle(
        GetCompletedDocumentsQuery request, CancellationToken cancellationToken)
    {
        return _documentCompletionService.GetCompletedDocuments(request.PaginationSearchModel);
    }
}