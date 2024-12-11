using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Application.Handlers.AddTranslate;
using EnglishTutorAI.Application.Handlers.DeleteTranslate;
using EnglishTutorAI.Application.Handlers.GetCompletedTranslates;
using EnglishTutorAI.Application.Handlers.GetConversationThread;
using EnglishTutorAI.Application.Handlers.GetNextTranslate;
using EnglishTutorAI.Application.Handlers.GetTranslateDetails;
using EnglishTutorAI.Application.Handlers.GetTranslates;
using EnglishTutorAI.Application.Handlers.HandleTranslateCompletion;
using EnglishTutorAI.Application.Handlers.HandleTranslateStart;
using EnglishTutorAI.Application.Handlers.SaveProgress;
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
public class TranslateController : ControllerBase
{
    private readonly IMediator _mediator;

    public TranslateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Routes.Translate.AddTranslate)]
    [Authorize(Roles = UserRoles.Admin)]
    public Task AddTranslate(TranslateCreationRequest request)
    {
        return _mediator.Send(new AddTranslateCommand(request));
    }

    [HttpGet(Routes.Translate.GetTranslate)]
    public Task<SearchResult<TranslateListItem>> Get([FromQuery]TranslateSearchModel model)
    {
        return _mediator.Send(new GetTranslatesQuery(model));
    }

    [HttpGet(Routes.Translate.GetTranslateDetails)]
    public async Task<IActionResult> GetTranslateDetails(string id)
    {
        if (!Guid.TryParse(id, out var translateId))
        {
            return NotFound("Translate not found.");
        }

        var translateResponse = await _mediator.Send(new GetTranslateDetailsQuery(translateId));

        return Ok(translateResponse);
    }

    [HttpPost(Routes.Translate.SaveCurrentLine)]
    public Task SaveCurrentLine(SaveCurrentLineRequest request)
    {
        return _mediator.Send(new SaveCurrentLineCommand(request));
    }

    [HttpGet(Routes.Translate.GetConversationThread)]
    public Task<IEnumerable<ChatMessageResponse>> GetConversationThread(string threadId)
    {
        return _mediator.Send(new GetConversationThreadCommand(threadId));
    }

    [HttpPost(Routes.Translate.DeleteTranslate)]
    [Authorize(Roles = UserRoles.Admin)]
    public Task DeleteTranslate(Guid translateId)
    {
        return _mediator.Send(new DeleteTranslateCommand(translateId));
    }

    [HttpPost(Routes.Translate.HandleTranslateCompletion)]
    public Task HandleTranslateCompletion(Guid userTranslateId)
    {
        return _mediator.Send(new HandleTranslateCompletionCommand(userTranslateId));
    }

    [HttpPost(Routes.Translate.HandleTranslateStart)]
    public Task HandleTranslateStart(Guid userTranslateId)
    {
        return _mediator.Send(new HandleTranslateStartCommand(userTranslateId));
    }

    [HttpGet(Routes.Translate.GetNextTranslate)]
    public Task<TranslateListItem?> GetNextTranslate([FromQuery]NextTranslateSearchModel model)
    {
        return _mediator.Send(new GetNextTranslateQuery(model));
    }

    [HttpGet(Routes.Translate.GetCompletedTranslates)]
    public Task<SearchResult<CompletedTranslateListItem>> GetCompletedTranslates([FromQuery]PaginationSearchModel model)
    {
        return _mediator.Send(new GetCompletedTranslatesQuery(model));
    }
}