using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.DeleteDocument;

public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand>
{
    private readonly IDeleteDocumentService _documentService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDocumentCommandHandler(IDeleteDocumentService documentService, IUnitOfWork unitOfWork)
    {
        _documentService = documentService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
    {
        await _documentService.Delete(request.DocumentId);
        await _unitOfWork.Commit();
    }
}