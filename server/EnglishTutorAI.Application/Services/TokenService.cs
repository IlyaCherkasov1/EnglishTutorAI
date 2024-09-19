using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Application.Utils;
using EnglishTutorAI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshTokenCookieService _refreshTokenCookieService;
    private readonly ISessionService _sessionService;
    private readonly UserManager<User> _userManager;

    public TokenService(
        IOptions<JwtSettings> jwtSettings,
        IRefreshTokenCookieService refreshTokenCookieService,
        UserManager<User> userManager,
        ISessionService sessionService)
    {
        _refreshTokenCookieService = refreshTokenCookieService;
        _userManager = userManager;
        _sessionService = sessionService;
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

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
        var newAccessToken = GenerateAccessToken(user!);

        return ResultBuilder.BuildSucceeded(newAccessToken);
    }
}