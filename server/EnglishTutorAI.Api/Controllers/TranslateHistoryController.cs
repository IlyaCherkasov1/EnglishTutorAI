using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.GetMistakeItem;
using EnglishTutorAI.Application.Handlers.GetTranslateMistakeHistory;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Translates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiRoute]
[Authorize]
[ApiController]
public class TranslateHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public TranslateHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.TranslateHistory.GetMistakesHistory)]
    public Task<SearchResult<MistakeHistoryItems>> GetMistakeHistoryItems([FromQuery]PaginationSearchModel model)
    {
        return _mediator.Send(new GetMistakeHistoryItemQuery(model));
    }

    [HttpGet(Routes.TranslateHistory.GetTranslateSessionMistakesHistory)]
    public Task<IEnumerable<TranslateMistakeHistoryItems>> GetTranslateSessionMistakeHistory(Guid userTranslateId)
    {
        return _mediator.Send(new GetTranslateMistakeHistoryQuery(userTranslateId));
    }
}