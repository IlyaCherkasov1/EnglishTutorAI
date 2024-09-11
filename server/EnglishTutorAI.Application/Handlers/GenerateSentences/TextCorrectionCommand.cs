using EnglishTutorAI.Application.Models.TextGeneration;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class TextCorrectionCommand : IRequest<(bool IsCorrected, string CorrectedText)>
{
    public TextCorrectionCommand(TextGenerationRequest request)
    {
        TextGenerationRequest = request;
    }

    public TextGenerationRequest TextGenerationRequest { get; }
}