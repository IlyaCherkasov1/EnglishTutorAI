using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.RestartDocumentSession;
using EnglishTutorAI.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiRoute]
[Authorize]
[ApiController]
public class DocumentSessionController : ControllerBase
{
    private readonly IMediator _mediator;

    public DocumentSessionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Routes.DocumentSession.RestartDocumentSession)]
    public async Task<Guid> RestartDocumentSession(RestartDocumentSessionRequest request)
    {
        return await _mediator.Send(new RestartDocumentSessionCommand(request));
    }
}