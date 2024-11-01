using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Documents;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetMistakeItem;

public class GetMistakeHistoryItemQueryHandler : IRequestHandler<GetMistakeHistoryItemQuery, IEnumerable<MistakeHistoryItems>>
{
    private readonly IMistakeHistorySearchService _makMistakeHistorySearchService;

    public GetMistakeHistoryItemQueryHandler(IMistakeHistorySearchService makMistakeHistorySearchService)
    {
        _makMistakeHistorySearchService = makMistakeHistorySearchService;
    }

    public Task<IEnumerable<MistakeHistoryItems>> Handle(
        GetMistakeHistoryItemQuery request, CancellationToken cancellationToken)
    {
        return _makMistakeHistorySearchService.Search(request.Model);
    }
}