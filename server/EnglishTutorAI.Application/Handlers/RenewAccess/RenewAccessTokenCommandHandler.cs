using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.RenewAccess;

public class RenewAccessTokenCommandHandler : IRequestHandler<RenewAccessTokenCommand, Result<string>>
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public RenewAccessTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<Result<string>> Handle(RenewAccessTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await _tokenService.RenewAccessToken();
        await _unitOfWork.Commit();

        return result;
    }
}