using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Translates;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetMistakeItem;

public class GetMistakeHistoryItemQueryHandler : IRequestHandler<GetMistakeHistoryItemQuery, SearchResult<MistakeHistoryItems>>
{
    private readonly IMistakeHistorySearchService _makMistakeHistorySearchService;

    public GetMistakeHistoryItemQueryHandler(IMistakeHistorySearchService makMistakeHistorySearchService)
    {
        _makMistakeHistorySearchService = makMistakeHistorySearchService;
    }

    public Task<SearchResult<MistakeHistoryItems>> Handle(
        GetMistakeHistoryItemQuery request, CancellationToken cancellationToken)
    {
        return _makMistakeHistorySearchService.Search(request.Model);
    }
}