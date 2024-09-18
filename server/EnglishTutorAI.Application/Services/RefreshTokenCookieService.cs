using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class RefreshTokenCookieService : IRefreshTokenCookieService
{
    private const string RefreshTokenCookieName = "refreshToken";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RefreshTokenCookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetRefreshToken(string refreshToken, DateTime expirationDate)
    {
        var httpContext = GetHttpContext();
        httpContext.Response.Cookies.Append(
            RefreshTokenCookieName, refreshToken, GetRefreshTokenCookieOptions(expirationDate));
    }

    public string? GetRefreshToken()
    {
        var httpContext = GetHttpContext();
        return httpContext.Request.Cookies.TryGetValue(RefreshTokenCookieName, out var refreshTokenValue)
            ? refreshTokenValue
            : null;
    }

    public void DeleteRefreshToken()
    {
        var httpContext = GetHttpContext();
        httpContext.Response.Cookies.Delete(RefreshTokenCookieName);
    }

    private static CookieOptions GetRefreshTokenCookieOptions(DateTime expirationDate)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            Expires = expirationDate,
            SameSite = SameSiteMode.Lax,
            Path = "/api/identity",
        };
    }

    private HttpContext GetHttpContext()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            throw new InvalidOperationException("HttpContext is not available.");
        }

        return httpContext;
    }
}