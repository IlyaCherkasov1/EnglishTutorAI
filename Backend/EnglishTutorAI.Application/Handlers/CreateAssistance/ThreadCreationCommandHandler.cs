using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.CreateAssistance;

public class ThreadCreationCommandHandler : IRequestHandler<ThreadCreationCommand, ThreadCreationResponse>
{
    private readonly IThreadCreationService _threadCreationService;
    private readonly IUnitOfWork _unitOfWork;

    public ThreadCreationCommandHandler(IThreadCreationService threadCreationService, IUnitOfWork unitOfWork)
    {
        _threadCreationService = threadCreationService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ThreadCreationResponse> Handle(ThreadCreationCommand request, CancellationToken cancellationToken)
    {
        var response = await _threadCreationService.Create(request.DocumentId);
        await _unitOfWork.Commit();

        return response;
    }
}