using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetCompletedTranslates;

public class GetCompletedTranslatesQuery : IRequest<SearchResult<CompletedTranslateListItem>>
{
    public GetCompletedTranslatesQuery(PaginationSearchModel paginationSearchModel)
    {
        PaginationSearchModel = paginationSearchModel;
    }

    public PaginationSearchModel PaginationSearchModel { get; set; }
}