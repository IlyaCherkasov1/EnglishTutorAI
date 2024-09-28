using EnglishTutorAI.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace EnglishTutorAI.Application.Handlers.ExternalLogin;

public class ExternalLoginCommandHandler : IRequestHandler<ExternalLoginCommand, AuthenticationProperties>
{
    private readonly IExternalLoginService _externalLoginService;

    public ExternalLoginCommandHandler(IExternalLoginService externalLoginService)
    {
        _externalLoginService = externalLoginService;
    }

    public Task<AuthenticationProperties> Handle(
        ExternalLoginCommand request,
        CancellationToken cancellationToken)
    {
        return _externalLoginService.Configure(request.Provider, request.RedirectUrl);
    }
}