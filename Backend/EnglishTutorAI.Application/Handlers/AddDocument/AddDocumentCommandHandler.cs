using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.AddDocument;

public class AddDocumentCommandHandler : IRequestHandler<AddDocumentCommand>
{
    private readonly IDocumentCreationService _documentCreationService;
    private readonly IUnitOfWork _unitOfWork;

    public AddDocumentCommandHandler(IDocumentCreationService documentCreationService, IUnitOfWork unitOfWork)
    {
        _documentCreationService = documentCreationService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddDocumentCommand request, CancellationToken cancellationToken)
    {
        await _documentCreationService.AddDocument(request.CreationRequest);
        await _unitOfWork.Commit();
    }
}