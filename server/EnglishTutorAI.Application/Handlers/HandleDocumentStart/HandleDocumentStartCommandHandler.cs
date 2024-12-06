using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.HandleDocumentStart;

public class HandleDocumentStartCommandHandler : IRequestHandler<HandleDocumentStartCommand>
{
    private readonly IDocumentStartAgainService _documentStartAgainService;
    private readonly IUnitOfWork _unitOfWork;

    public HandleDocumentStartCommandHandler(IDocumentStartAgainService documentStartAgainService, IUnitOfWork unitOfWork)
    {
        _documentStartAgainService = documentStartAgainService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(HandleDocumentStartCommand request, CancellationToken cancellationToken)
    {
        await _documentStartAgainService.StartAgain(request.UserDocumentId);
        await _unitOfWork.Commit();
    }
}