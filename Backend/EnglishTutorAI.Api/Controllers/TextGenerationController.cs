using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.GenerateSentences;
using EnglishTutorAI.Application.Models.TextGeneration;
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

    [HttpPost(Routes.Assistant.GenerateChatCompletion)]
    public async Task<IActionResult> CorrectText(TextGenerationRequest request)
    {
        var (isCorrected, correctedText) = await _mediator.Send(new TextCorrectionCommand(request));

        return Ok(new { IsCorrected = isCorrected, CorrectedText = correctedText });
    }
}