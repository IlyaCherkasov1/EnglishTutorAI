using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class GenerateSentencesCommandHandler : IRequestHandler<GenerateSentencesCommand>
{
    private readonly IOpenAiService _openAiService;

    public GenerateSentencesCommandHandler(IOpenAiService openAiService)
    {
        _openAiService = openAiService;
    }

    public async Task Handle(GenerateSentencesCommand request, CancellationToken cancellationToken)
    {
        await _openAiService.GenerateSentences(request.Phrase);
    }
}