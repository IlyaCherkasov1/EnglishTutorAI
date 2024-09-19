using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class SessionService : ISessionService
{
    private readonly IRepository<UserSession> _userSession;
    private readonly IRefreshTokenCookieService _refreshTokenCookieService;
    private readonly ITokenGeneratorService _tokenGeneratorService;

    public SessionService(
        IRepository<UserSession> userSession,
        IRefreshTokenCookieService refreshTokenCookieService,
        ITokenGeneratorService tokenGeneratorService)
    {
        _userSession = userSession;
        _refreshTokenCookieService = refreshTokenCookieService;
        _tokenGeneratorService = tokenGeneratorService;
    }

    public async Task<UserSession> CreateSession(Guid userId)
    {
        var session = new UserSession
        {
            UserId = userId,
            Expires = DateTime.UtcNow.AddDays(7),
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
        session.Expires = DateTime.UtcNow.AddDays(30);
        session.RefreshToken = _tokenGeneratorService.GenerateRefreshToken();

        _userSession.Update(session);
        _refreshTokenCookieService.SetRefreshToken(session.RefreshToken, session.Expires);
    }
}