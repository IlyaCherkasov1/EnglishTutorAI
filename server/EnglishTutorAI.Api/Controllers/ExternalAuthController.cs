using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.ExternalLogin;
using EnglishTutorAI.Application.Handlers.ExternalLoginCallback;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiRoute]
[ApiController]
public class ExternalAuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExternalAuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Routes.ExternalAuth.ExternalLogin)]
    public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
    {
        var redirectUrl = $"{Routes.Urls.ExternalAuthCallbackUrl}?returnUrl={returnUrl}";
        var properties = await _mediator.Send(new ExternalLoginCommand(provider, redirectUrl));

        return Challenge(properties, provider);
    }

    [HttpGet(Routes.ExternalAuth.ExternalLoginCallback)]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl)
    {
        var result = await _mediator.Send(new ExternalLoginCallbackCommand());

        if (!result.IsSucceeded)
        {
            return StatusCode(401, new { message = string.Join(", ", result.Errors!) });
        }

        return Redirect(returnUrl);
    }
}