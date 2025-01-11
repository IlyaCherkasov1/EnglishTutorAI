using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class ExternalLoginService : IExternalLoginService
{
    private readonly SignInManager<User> _signInManager;

    public ExternalLoginService(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public Task<AuthenticationProperties> Configure(string provider, string redirectUrl)
    {
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        return Task.FromResult(properties);
    }
}