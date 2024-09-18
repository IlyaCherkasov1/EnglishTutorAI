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
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshTokenCookieService _refreshTokenCookieService;
    private readonly IRepository<RefreshToken> _refreshTokenRepository;

    public TokenService(
        IOptions<JwtSettings> jwtSettings,
        IRefreshTokenCookieService refreshTokenCookieService,
        IRepository<RefreshToken> refreshTokenRepository)
    {
        _refreshTokenCookieService = refreshTokenCookieService;
        _refreshTokenRepository = refreshTokenRepository;
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

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[128];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    public async Task<Result<string>> RenewAccessToken()
    {
        var refreshToken = _refreshTokenCookieService.GetRefreshToken();

        if (refreshToken == null)
        {
            return ResultBuilder.BuildFailed<string>("refresh token does not exist");
        }

        var refreshTokenEntity = await _refreshTokenRepository
            .GetSingleOrDefault(new RefreshTokenByValueSpecification(refreshToken));

        if (refreshTokenEntity == null || refreshTokenEntity.IsExpired)
        {
            return ResultBuilder.BuildFailed<string>("Invalid or expired refresh token");
        }

        var user = refreshTokenEntity.User;

        var newAccessToken = GenerateAccessToken(user);
        var newRefreshToken = GenerateRefreshToken();

        refreshTokenEntity.Token = newRefreshToken;
        refreshTokenEntity.Expires = DateTime.UtcNow.AddDays(7);

        _refreshTokenCookieService.SetRefreshToken(
            newRefreshToken,
            refreshTokenEntity.Expires);

        return ResultBuilder.BuildSucceeded(newAccessToken);
    }
}