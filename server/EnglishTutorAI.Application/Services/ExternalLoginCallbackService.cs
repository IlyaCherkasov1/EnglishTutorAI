using System.Security.Claims;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Utils;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class ExternalLoginCallbackService : IExternalLoginCallbackService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ISessionService _sessionService;

    public ExternalLoginCallbackService(
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        ISessionService sessionService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _sessionService = sessionService;
    }

    public async Task<Result> Login()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        if (info == null)
        {
            return ResultBuilder.BuildFailed("Failed to fetch external login information");
        }

        var email = info.Principal.FindFirstValue(ClaimTypes.Email);

        if (string.IsNullOrEmpty(email))
        {
            return ResultBuilder.BuildFailed("Failed to create user: required data is missing");
        }

        var user = await GetOrCreateUser(email, info);

        return await TrySignInAndCreateSession(info, user);
    }

    private async Task<User> GetOrCreateUser(string email, ExternalLoginInfo info)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new User()
            {
                FirstName = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? "Anonymous",
                UserName = email,
                Email = email
            };

            await _userManager.CreateAsync(user);
            await _userManager.AddLoginAsync(user, info);
        }

        return user;
    }

    private async Task<Result> TrySignInAndCreateSession(ExternalLoginInfo info, User user)
    {
        var signinResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

        if (!signinResult.Succeeded)
        {
            return ResultBuilder.BuildFailed("Failed to signs in a user via third party login");
        }

        await _sessionService.CreateSession(user.Id);

        return ResultBuilder.BuildSucceeded();
    }
}