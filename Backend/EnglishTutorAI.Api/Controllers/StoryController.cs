using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.GetStories;
using EnglishTutorAI.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiController]
[ApiRoute]
public class StoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public StoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.Story.GetStory)]
    public Task<StoryResponse> GetStory()
    {
        return _mediator.Send(new GetStoryQuery());
    }
}