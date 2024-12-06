using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DocumentStartAgainService : IDocumentStartAgainService
{
    private readonly IRepository<UserDocument> _userDocumentRepository;

    public DocumentStartAgainService(IRepository<UserDocument> userDocumentRepository)
    {
        _userDocumentRepository = userDocumentRepository;
    }

    public async Task StartAgain(Guid userDocumentId)
    {
        var userDocument = await _userDocumentRepository.GetById(userDocumentId);

        userDocument.CurrentLine = 0;
        userDocument.IsCompleted = false;
        userDocument.SessionId = Guid.NewGuid();
    }
}