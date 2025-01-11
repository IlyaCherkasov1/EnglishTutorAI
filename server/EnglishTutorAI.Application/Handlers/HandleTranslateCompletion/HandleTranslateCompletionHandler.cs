using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.HandleTranslateCompletion;

public class HandleTranslateCompletionHandler : IRequestHandler<HandleTranslateCompletionCommand>
{
    private readonly ITranslateCompletionService _translateCompletionService;
    private readonly IUnitOfWork _unitOfWork;

    public HandleTranslateCompletionHandler(
        ITranslateCompletionService translateCompletionService,
        IUnitOfWork unitOfWork)
    {
        _translateCompletionService = translateCompletionService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(HandleTranslateCompletionCommand request, CancellationToken cancellationToken)
    {
        await _translateCompletionService.Save(request.UserTranslateId);
        await _unitOfWork.Commit();
    }
}