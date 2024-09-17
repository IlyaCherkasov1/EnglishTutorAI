using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutCommandHandler(IIdentityService identityService, IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _identityService.Logout();
        await _unitOfWork.Commit();
    }
}