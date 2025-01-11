using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Common;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.ExternalLoginCallback;

public class ExternalLoginCallbackCommandHandler : IRequestHandler<ExternalLoginCallbackCommand, Result>
{
    private readonly IExternalLoginCallbackService _externalLoginCallbackService;
    private readonly IUnitOfWork _unitOfWork;

    public ExternalLoginCallbackCommandHandler(IExternalLoginCallbackService externalLoginCallbackService, IUnitOfWork unitOfWork)
    {
        _externalLoginCallbackService = externalLoginCallbackService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ExternalLoginCallbackCommand request, CancellationToken cancellationToken)
    {
        var result = await _externalLoginCallbackService.Login();
        await _unitOfWork.Commit();

        return result;
    }
}