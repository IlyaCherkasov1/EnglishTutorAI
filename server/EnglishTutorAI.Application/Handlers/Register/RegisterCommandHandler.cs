using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IIdentityService identityService, IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        using var transaction = _unitOfWork.BeginTransaction();
        var result = await _identityService.RegisterUser(request.Request);

        if (result.IsSucceeded)
        {
            transaction.Commit();
        }

        return result;
    }
}