using Microsoft.AspNetCore.Http;

namespace EnglishTutorAI.Application.Interfaces;

public interface IRefreshTokenCookieService
{
    public void SetRefreshToken(string refreshToken, DateTime expirationDate);

    public string? GetRefreshToken();

    public void DeleteRefreshToken();
}