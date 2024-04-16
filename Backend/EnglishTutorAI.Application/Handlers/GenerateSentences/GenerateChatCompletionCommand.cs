using EnglishTutorAI.Application.Models.TextGeneration;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class GenerateChatCompletionCommand : IRequest<string>
{
    public GenerateChatCompletionCommand(TextGenerationRequest request)
    {
        TextGenerationRequest = request;
    }

    public TextGenerationRequest TextGenerationRequest { get; }
}