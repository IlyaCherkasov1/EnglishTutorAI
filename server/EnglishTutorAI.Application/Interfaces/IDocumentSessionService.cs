namespace EnglishTutorAI.Application.Interfaces;

public interface IDocumentSessionService
{
    Task<Guid> StartNewSession(Guid userDocumentId);
}