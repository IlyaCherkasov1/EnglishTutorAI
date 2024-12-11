using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetCompletedTranslates;

public class GetCompletedTranslatesQueryHandler :
    IRequestHandler<GetCompletedTranslatesQuery, SearchResult<CompletedTranslateListItem>>
{
    private readonly ITranslateCompletionService _translateCompletionService;

    public GetCompletedTranslatesQueryHandler(ITranslateCompletionService translateCompletionService)
    {
        _translateCompletionService = translateCompletionService;
    }

    public Task<SearchResult<CompletedTranslateListItem>> Handle(
        GetCompletedTranslatesQuery request, CancellationToken cancellationToken)
    {
        return _translateCompletionService.GetCompletedTranslates(request.PaginationSearchModel);
    }
}