using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Translates;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetNextTranslate;

public class GetNextTranslateQuery : IRequest<TranslateListItem?>
{
    public GetNextTranslateQuery(NextTranslateSearchModel model)
    {
        Model = model;
    }

    public NextTranslateSearchModel Model { get; set; }
}