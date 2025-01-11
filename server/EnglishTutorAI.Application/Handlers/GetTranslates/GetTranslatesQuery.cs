using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Translates;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetTranslates;

public class GetTranslatesQuery : IRequest<SearchResult<TranslateListItem>>
{
    public GetTranslatesQuery(TranslateSearchModel model)
    {
        Model = model;
    }

    public TranslateSearchModel Model { get; }
}