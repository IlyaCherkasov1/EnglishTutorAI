using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers;
using EnglishTutorAI.Application.Handlers.GetStoryCount;
using EnglishTutorAI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiController]
[ApiRoute]
public class SentencesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SentencesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.Sentence.GetSentence)]
    public Task<IReadOnlyList<Sentence>> GetSentences()
    {
        return _mediator.Send(new GetSentencesQuery());
    }
}