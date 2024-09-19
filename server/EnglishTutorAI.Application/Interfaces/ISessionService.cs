using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Interfaces;

public interface ISessionService
{
    Task<UserSession> CreateSession(Guid userId);

    Task<UserSession?> GetValidSession(string refreshToken);

    Task InvalidateSessions(Guid userId);

    void UpdateRefreshToken(UserSession userSession);
}