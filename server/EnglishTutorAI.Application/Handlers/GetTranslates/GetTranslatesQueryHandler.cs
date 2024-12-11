using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetTranslates;

public class GetTranslatesQueryHandler : IRequestHandler<GetTranslatesQuery, SearchResult<TranslateListItem>>
{
    private readonly ITranslateSearchService _translateSearchService;

    public GetTranslatesQueryHandler(ITranslateSearchService translateSearchService)
    {
        _translateSearchService = translateSearchService;
    }

    public Task<SearchResult<TranslateListItem>> Handle(GetTranslatesQuery request, CancellationToken cancellationToken)
    {
        return _translateSearchService.Search(request.Model);
    }
}