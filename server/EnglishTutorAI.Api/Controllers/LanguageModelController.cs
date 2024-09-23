using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Controllers.Attributes;
using EnglishTutorAI.Application.Handlers.GenerateSentences;
using EnglishTutorAI.Application.Handlers.SendMessageToAssistant;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.TextGeneration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnglishTutorAI.Api.Controllers;

[ApiRoute]
[Authorize]
[ApiController]
public class LanguageModelController : ControllerBase
{
    private readonly IMediator _mediator;

    public LanguageModelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Routes.Assistant.GenerateChatCompletion)]
    public async Task<IActionResult> CorrectText(TextGenerationRequest request)
    {
        var (isCorrected, correctedText) = await _mediator.Send(new TextCorrectionCommand(request));

        return Ok(new { IsCorrected = isCorrected, CorrectedText = correctedText });
    }

    [HttpPost(Routes.Assistant.SendMessage)]
    public async Task<string> SendMessage(SendMessageRequest request)
    {
        return await _mediator.Send(new SendMessageToAssistantCommand(request));
    }
}