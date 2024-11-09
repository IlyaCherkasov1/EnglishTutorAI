namespace EnglishTutorAI.Application.Interfaces;

public interface IDocumentSessionService
{
    Task<Guid> StartNewSession(Guid documentId);

    Task<Guid> RestartSession(Guid documentId, Guid currentSessionId);
}