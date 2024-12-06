using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.GetDocumentMistakeHistory;
using EnglishTutorAI.Application.Handlers.GetMistakeItem;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Documents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiRoute]
[Authorize]
[ApiController]
public class DocumentHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.DocumentHistory.GetMistakesHistory)]
    public Task<SearchResult<MistakeHistoryItems>> GetMistakeHistoryItems([FromQuery]PaginationSearchModel model)
    {
        return _mediator.Send(new GetMistakeHistoryItemQuery(model));
    }

    [HttpGet(Routes.DocumentHistory.GetDocumentSessionMistakesHistory)]
    public Task<IEnumerable<DocumentMistakeHistoryItems>> GetDocumentSessionMistakeHistory(Guid userDocumentId)
    {
        return _mediator.Send(new GetDocumentMistakeHistoryQuery(userDocumentId));
    }
}