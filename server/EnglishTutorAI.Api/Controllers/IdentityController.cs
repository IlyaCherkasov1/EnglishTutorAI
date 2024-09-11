using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.Login;
using EnglishTutorAI.Application.Handlers.Register;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Requests;
using EnglishTutorAI.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiRoute]
[Authorize]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;

    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Routes.Identity.Register)]
    [AllowAnonymous]
    public async Task<Result> Register([FromBody] UserRegisterRequest request)
    {
        var result = await _mediator.Send(new RegisterCommand(request));

        return result;
    }

    [HttpPost(Routes.Identity.Login)]
    [AllowAnonymous]
    public async Task<Result<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        var result = await _mediator.Send(new LoginCommand(request));

        return result;
    }
}