using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DocumentSessionService : IDocumentSessionService
{
    private readonly IRepository<DocumentSession> _documentSessionRepository;

    public DocumentSessionService(IRepository<DocumentSession> documentSessionRepository)
    {
        _documentSessionRepository = documentSessionRepository;
    }

    public async Task<Guid> StartNewSession(Guid documentId)
    {
        var newSession = new DocumentSession { DocumentId = documentId, };
        var session = await _documentSessionRepository.Add(newSession);

        return session.Id;
    }

    public async Task<Guid> RestartSession(Guid documentId, Guid currentSessionId)
    {
        var currentSession = await _documentSessionRepository.GetById(currentSessionId);
        _documentSessionRepository.Delete(currentSession);

        var newSession = new DocumentSession { DocumentId = documentId, };
        await _documentSessionRepository.Add(newSession);

        return newSession.Id;
    }
}