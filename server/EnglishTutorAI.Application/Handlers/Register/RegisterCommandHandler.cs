using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Application.Handlers.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return _identityService.RegisterUser(request.Request);
    }
}