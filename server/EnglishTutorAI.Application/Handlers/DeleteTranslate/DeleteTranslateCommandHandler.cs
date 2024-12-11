using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.DeleteTranslate;

public class DeleteTranslateCommandHandler : IRequestHandler<DeleteTranslateCommand>
{
    private readonly ITranslateDeletionService _translateDeletionService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTranslateCommandHandler(ITranslateDeletionService translateDeletionService, IUnitOfWork unitOfWork)
    {
        _translateDeletionService = translateDeletionService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteTranslateCommand request, CancellationToken cancellationToken)
    {
        await _translateDeletionService.Delete(request.TranslateId);
        await _unitOfWork.Commit();
    }
}