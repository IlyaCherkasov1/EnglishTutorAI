using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Utils;
using EnglishTutorAI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class JwtAuthService : IJwtAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshTokenCookieService _refreshTokenCookieService;
    private readonly ISessionService _sessionService;
    private readonly UserManager<User> _userManager;
    private readonly IClaimsService _claimsService;

    public JwtAuthService(
        IOptions<JwtSettings> jwtSettings,
        IRefreshTokenCookieService refreshTokenCookieService,
        UserManager<User> userManager,
        ISessionService sessionService,
        IClaimsService claimsService)
    {
        _refreshTokenCookieService = refreshTokenCookieService;
        _userManager = userManager;
        _sessionService = sessionService;
        _claimsService = claimsService;
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateAccessToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtSettings.AccessTokenExpiryMinutes)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<Result<string>> RenewAccessToken()
    {
        var refreshToken = _refreshTokenCookieService.GetRefreshToken();

        if (refreshToken == null)
        {
            return ResultBuilder.BuildFailed<string>("refresh token does not exist");
        }

        var session = await _sessionService.GetValidSession(refreshToken);

        if (session == null)
        {
            return ResultBuilder.BuildFailed<string>("session is not valid");
        }

        _sessionService.UpdateRefreshToken(session);
        var user = await _userManager.FindByIdAsync(session.UserId.ToString());

        if (user == null)
        {
            return ResultBuilder.BuildFailed<string>("user not found");
        }

        var claims = _claimsService.CreateUserClaims(user);
        var newAccessToken = GenerateAccessToken(claims);

        return ResultBuilder.BuildSucceeded(newAccessToken);
    }
}