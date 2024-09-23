using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class SessionService : ISessionService
{
    private readonly IRepository<UserSession> _userSession;
    private readonly IRefreshTokenCookieService _refreshTokenCookieService;
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly RefreshTokenSettings _refreshTokenSettings;

    public SessionService(
        IRepository<UserSession> userSession,
        IRefreshTokenCookieService refreshTokenCookieService,
        ITokenGeneratorService tokenGeneratorService,
        IOptions<RefreshTokenSettings> refreshTokenSettings)
    {
        _userSession = userSession;
        _refreshTokenCookieService = refreshTokenCookieService;
        _tokenGeneratorService = tokenGeneratorService;
        _refreshTokenSettings = refreshTokenSettings.Value;
    }

    public async Task<UserSession> CreateSession(Guid userId)
    {
        var session = new UserSession
        {
            UserId = userId,
            Expires = DateTime.UtcNow.AddDays(_refreshTokenSettings.ExpiryDays),
            RefreshToken = _tokenGeneratorService.GenerateRefreshToken(),
        };

        await _userSession.Add(session);
        _refreshTokenCookieService.SetRefreshToken(session.RefreshToken, session.Expires);

        return session;
    }

    public async Task<UserSession?> GetValidSession(string refreshToken)
    {
        return await _userSession.GetFirstOrDefault(new ValidUserSessionByRefreshTokenSpecification(refreshToken));
    }

    public async Task InvalidateSessions(Guid userId)
    {
        var sessions = await _userSession.List(new ValidUserSessionByUserIdSpecification(userId));

        foreach (var session in sessions)
        {
            session.IsValid = false;
        }

        _refreshTokenCookieService.DeleteRefreshToken();
    }

    public void UpdateRefreshToken(UserSession session)
    {
        session.Expires = DateTime.UtcNow.AddDays(_refreshTokenSettings.ExpiryDays);
        session.RefreshToken = _tokenGeneratorService.GenerateRefreshToken();

        _userSession.Update(session);
        _refreshTokenCookieService.SetRefreshToken(session.RefreshToken, session.Expires);
    }
}