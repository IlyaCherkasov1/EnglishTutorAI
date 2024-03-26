using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.GenerateSentences;
using EnglishTutorAI.Application.Interfaces;
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
    public async Task<IActionResult> GenerateSentences(string text)
    {
        var result = await _mediator.Send(new GenerateSentencesCommand(text));
        return Ok(result);
    }
}