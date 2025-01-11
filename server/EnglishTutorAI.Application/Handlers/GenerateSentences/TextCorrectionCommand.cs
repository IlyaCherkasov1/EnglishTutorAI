using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.TextGeneration;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class TextCorrectionCommand : IRequest<TextCorrectionResult>
{
    public TextCorrectionCommand(TextGenerationRequest request)
    {
        TextGenerationRequest = request;
    }

    public TextGenerationRequest TextGenerationRequest { get; }
}