using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Translates;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetMistakeItem;

public class GetMistakeHistoryItemQuery : IRequest<SearchResult<MistakeHistoryItems>>
{
    public GetMistakeHistoryItemQuery(PaginationSearchModel model)
    {
        Model = model;
    }

    public PaginationSearchModel Model { get; set; }
}