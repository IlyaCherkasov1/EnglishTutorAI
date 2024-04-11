using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class GenerateChatCompletionCommandHandler : IRequestHandler<GenerateChatCompletionCommand, string>
{
    private readonly IOpenAiService _openAiService;

    public GenerateChatCompletionCommandHandler(IOpenAiService openAiService)
    {
        _openAiService = openAiService;
    }

    public async Task<string> Handle(GenerateChatCompletionCommand request, CancellationToken cancellationToken)
    {
        return await _openAiService.GenerateChatCompletion(request.Text);
    }
}