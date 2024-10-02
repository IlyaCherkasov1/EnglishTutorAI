using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.RenewAccess;

public class RenewAccessTokenCommandHandler : IRequestHandler<RenewAccessTokenCommand, Result<string>>
{
    private readonly IJwtAuthService _jwtAuthService;
    private readonly IUnitOfWork _unitOfWork;

    public RenewAccessTokenCommandHandler(IUnitOfWork unitOfWork, IJwtAuthService jwtAuthService)
    {
        _unitOfWork = unitOfWork;
        _jwtAuthService = jwtAuthService;
    }

    public async Task<Result<string>> Handle(RenewAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await _jwtAuthService.RenewAccessToken();

        await _unitOfWork.Commit();

        return result;
    }
}