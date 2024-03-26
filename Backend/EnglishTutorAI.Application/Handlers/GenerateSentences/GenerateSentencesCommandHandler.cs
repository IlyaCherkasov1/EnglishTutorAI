using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class GenerateSentencesCommandHandler : IRequestHandler<GenerateSentencesCommand, string>
{
    private readonly IOpenAiService _openAiService;

    public GenerateSentencesCommandHandler(IOpenAiService openAiService)
    {
        _openAiService = openAiService;
    }

    public async Task<string> Handle(GenerateSentencesCommand request, CancellationToken cancellationToken)
    {
        return await _openAiService.GenerateSentences(request.Text);
    }
}