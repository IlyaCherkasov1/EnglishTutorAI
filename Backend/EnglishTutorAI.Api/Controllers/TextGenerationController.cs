using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.GenerateSentences;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiController]
[ApiRoute]
public class TextGenerationController : ControllerBase
{
    private readonly IMediator _mediator;

    public TextGenerationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Routes.Assistant.GenerateSentences)]
    public async Task<IActionResult> GenerateChatCompletionCommand(string text)
    {
        var result = await _mediator.Send(new GenerateChatCompletionCommand(text));
        return Ok(result);
    }
}