using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GenerateSentences;

public class TextCorrectionCommandHandler : IRequestHandler<TextCorrectionCommand, TextCorrectionResult>
{
    private readonly ITextCorrectionService _textCorrectionService;
    private readonly IUnitOfWork _unitOfWork;

    public TextCorrectionCommandHandler(ITextCorrectionService textCorrectionService, IUnitOfWork unitOfWork)
    {
        _textCorrectionService = textCorrectionService;
        _unitOfWork = unitOfWork;
    }

    public async Task<TextCorrectionResult> Handle(TextCorrectionCommand request, CancellationToken cancellationToken)
    {
        var result = await _textCorrectionService.Correct(request.TextGenerationRequest);
        await _unitOfWork.Commit();

        return result;
    }
}