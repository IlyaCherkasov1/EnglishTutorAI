using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Requests;
using EnglishTutorAI.Application.Utils;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenCookieService _refreshTokenCookieService;
    private readonly ISessionService _sessionService;

    public IdentityService(
        UserManager<User> userManager,
        ITokenService tokenService,
        IRefreshTokenCookieService refreshTokenCookieService,
        ISessionService sessionService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _refreshTokenCookieService = refreshTokenCookieService;
        _sessionService = sessionService;
    }

    public async Task<Result> RegisterUser(UserRegisterRequest model)
    {
        var user = new User
        {
            FirstName = model.FirstName,
            Email = model.Email,
            UserName = model.Email,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return ResultBuilder.BuildFailed(result.Errors.Select(e => e.Description));
        }

        return ResultBuilder.BuildSucceeded();
    }

    public async Task<Result<string>> LoginUser(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return ResultBuilder.BuildFailed<string>("There is no user with that Email address");
        }

        var result = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
        {
            return ResultBuilder.BuildFailed<string>("Invalid password");
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        await _sessionService.CreateSession(user.Id);

        return ResultBuilder.BuildSucceeded(accessToken);
    }

    public async Task Logout()
    {
        var refreshToken = _refreshTokenCookieService.GetRefreshToken();

        if (refreshToken != null)
        {
            var session = await _sessionService.GetValidSession(refreshToken);

            if (session != null)
            {
                await _sessionService.InvalidateSessions(session.UserId);
            }
        }
    }
}