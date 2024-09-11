using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class TextCorrectionCommandHandler : IRequestHandler<TextCorrectionCommand, (bool IsCorrected, string CorrectedText)>
{
    private readonly ITextCorrectionService _textCorrectionService;
    private readonly IUnitOfWork _unitOfWork;

    public TextCorrectionCommandHandler(ITextCorrectionService textCorrectionService, IUnitOfWork unitOfWork)
    {
        _textCorrectionService = textCorrectionService;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool IsCorrected, string CorrectedText)> Handle(TextCorrectionCommand request, CancellationToken cancellationToken)
    {
        var result = await _textCorrectionService.Correct(request.TextGenerationRequest);
        await _unitOfWork.Commit();

        return result;
    }
}