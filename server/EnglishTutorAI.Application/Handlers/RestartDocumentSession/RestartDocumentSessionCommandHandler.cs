using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.RestartDocumentSession;

public class RestartDocumentSessionCommandHandler : IRequestHandler<RestartDocumentSessionCommand, Guid>
{
    private readonly IDocumentSessionService _documentSessionService;
    private readonly IUnitOfWork _unitOfWork;

    public RestartDocumentSessionCommandHandler(IDocumentSessionService documentSessionService, IUnitOfWork unitOfWork)
    {
        _documentSessionService = documentSessionService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(RestartDocumentSessionCommand command, CancellationToken cancellationToken)
    {
        var result = await _documentSessionService.RestartSession(
            command.Request.DocumentId, command.Request.CurrentSessionId);

        await _unitOfWork.Commit();

        return result;
    }
}