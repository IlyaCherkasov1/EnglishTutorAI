using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.SaveProgress;

public class SaveCurrentLineCommandHandler : IRequestHandler<SaveCurrentLineCommand>
{
    private readonly ISaveCurrentLineService _saveCurrentLineService;
    private readonly IUnitOfWork _unitOfWork;

    public SaveCurrentLineCommandHandler(ISaveCurrentLineService saveCurrentLineService, IUnitOfWork unitOfWork)
    {
        _saveCurrentLineService = saveCurrentLineService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(SaveCurrentLineCommand request, CancellationToken cancellationToken)
    {
        await _saveCurrentLineService.SaveCurrentLine(request.SaveCurrentLineRequest);
        await _unitOfWork.Commit();
    }
}