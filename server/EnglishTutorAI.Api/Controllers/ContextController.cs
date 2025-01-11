using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.LoadContext;
using EnglishTutorAI.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiController]
[ApiRoute]
public class ContextController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContextController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public Task<ContextResponse> Load()
    {
        return _mediator.Send(new LoadContextQuery());
    }
}