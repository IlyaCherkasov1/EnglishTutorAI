using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.Login;
using EnglishTutorAI.Application.Handlers.Logout;
using EnglishTutorAI.Application.Handlers.Register;
using EnglishTutorAI.Application.Handlers.RenewAccess;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Requests;
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
        return await _mediator.Send(new RegisterCommand(request));
    }

    [HttpPost(Routes.Identity.Login)]
    [AllowAnonymous]
    public async Task<Result<string>> Login([FromBody] LoginRequest request)
    {
        return await _mediator.Send(new LoginCommand(request));
    }

    [HttpGet(Routes.Identity.RenewAccessToken)]
    [AllowAnonymous]
    public async Task<Result<string>> RenewAccessToken()
    {
        return await _mediator.Send(new RenewAccessTokenCommand());
    }

    [HttpGet(Routes.Identity.Logout)]
    public async Task Logout()
    {
        await _mediator.Send(new LogoutCommand());
    }
}