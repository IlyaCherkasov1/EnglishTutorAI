using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.HandleTranslateStart;

public class HandleTranslateStartCommandHandler : IRequestHandler<HandleTranslateStartCommand>
{
    private readonly ITranslateStartAgainService _translateStartAgainService;
    private readonly IUnitOfWork _unitOfWork;

    public HandleTranslateStartCommandHandler(ITranslateStartAgainService translateStartAgainService, IUnitOfWork unitOfWork)
    {
        _translateStartAgainService = translateStartAgainService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(HandleTranslateStartCommand request, CancellationToken cancellationToken)
    {
        await _translateStartAgainService.StartAgain(request.UserTranslateId);
        await _unitOfWork.Commit();
    }
}