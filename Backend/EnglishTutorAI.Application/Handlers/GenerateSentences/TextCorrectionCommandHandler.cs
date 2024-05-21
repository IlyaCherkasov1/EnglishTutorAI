using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class TextCorrectionCommandHandler : IRequestHandler<TextCorrectionCommand, (bool IsCorrected, string CorrectedText)>
{
    private readonly ITextCorrectionService _textCorrectionService;

    public TextCorrectionCommandHandler(ITextCorrectionService textCorrectionService)
    {
        _textCorrectionService = textCorrectionService;
    }

    public async Task<(bool IsCorrected, string CorrectedText)> Handle(TextCorrectionCommand request, CancellationToken cancellationToken)
    {
        return await _textCorrectionService.Correct(request.TextGenerationRequest);
    }
}