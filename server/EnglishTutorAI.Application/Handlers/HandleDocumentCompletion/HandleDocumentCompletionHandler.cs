using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.HandleDocumentCompletion;

public class HandleDocumentCompletionHandler : IRequestHandler<HandleDocumentCompletionCommand>
{
    private readonly IDocumentCompletionService _documentCompletionService;
    private readonly IUnitOfWork _unitOfWork;

    public HandleDocumentCompletionHandler(
        IDocumentCompletionService documentCompletionService,
        IUnitOfWork unitOfWork)
    {
        _documentCompletionService = documentCompletionService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(HandleDocumentCompletionCommand request, CancellationToken cancellationToken)
    {
        await _documentCompletionService.Save(request.DocumentId);
        await _unitOfWork.Commit();
    }
}