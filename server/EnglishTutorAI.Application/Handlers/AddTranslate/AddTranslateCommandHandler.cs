using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.AddTranslate;

public class AddTranslateCommandHandler : IRequestHandler<AddTranslateCommand>
{
    private readonly ITranslateCreationService _translateCreationService;
    private readonly IUnitOfWork _unitOfWork;

    public AddTranslateCommandHandler(ITranslateCreationService translateCreationService, IUnitOfWork unitOfWork)
    {
        _translateCreationService = translateCreationService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddTranslateCommand request, CancellationToken cancellationToken)
    {
        await _translateCreationService.AddTranslate(request.CreationRequest);
        await _unitOfWork.Commit();
    }
}