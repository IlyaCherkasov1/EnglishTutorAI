using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Constants;
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
    private readonly IJwtAuthService _jwtAuthService;
    private readonly IRefreshTokenCookieService _refreshTokenCookieService;
    private readonly ISessionService _sessionService;
    private readonly IClaimsService _claimsService;
    private readonly IUserAchievementSeeder _userAchievementSeeder;

    public IdentityService(
        UserManager<User> userManager,
        IRefreshTokenCookieService refreshTokenCookieService,
        ISessionService sessionService,
        IClaimsService claimsService,
        IJwtAuthService jwtAuthService,
        IUserAchievementSeeder userAchievementSeeder)
    {
        _userManager = userManager;
        _refreshTokenCookieService = refreshTokenCookieService;
        _sessionService = sessionService;
        _claimsService = claimsService;
        _jwtAuthService = jwtAuthService;
        _userAchievementSeeder = userAchievementSeeder;
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

        var roleResult = await _userManager.AddToRoleAsync(user, UserRoles.User);

        if (!roleResult.Succeeded)
        {
            return ResultBuilder.BuildFailed(roleResult.Errors.Select(e => e.Description));
        }

        await _userAchievementSeeder.Seed(user);

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

        var roles = await _userManager.GetRolesAsync(user);
        var claims = _claimsService.CreateUserClaims(user, roles);
        var accessToken = _jwtAuthService.GenerateAccessToken(claims);
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