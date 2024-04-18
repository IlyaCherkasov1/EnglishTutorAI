using AutoMapper;
using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.AddStory;
using EnglishTutorAI.Application.Handlers.GetStories;
using EnglishTutorAI.Application.Handlers.GetStoryCount;
using EnglishTutorAI.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiController]
[ApiRoute]
public class StoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public StoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet(Routes.Story.GetStoryByIndex)]
    public Task<StoryResponse> GetStory(int index)
    {
        return _mediator.Send(new GetStoryQuery(index));
    }

    [HttpGet(Routes.Story.Count)]
    public Task<int> GetStoryCount()
    {
        return _mediator.Send(new GetStoryCountQuery());
    }

    [HttpPost(Routes.Story.AddStory)]
    public Task AddStory(StoryCreationRequest request)
    {
        return _mediator.Send(new AddStoryCommand(_mapper.Map<StoryCreationRequest>(request)));
    }
}