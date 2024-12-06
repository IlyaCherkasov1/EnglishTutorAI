using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DocumentRetrievalService : IDocumentRetrievalService
{
    private readonly IRepository<UserDocument> _userDocumentRepository;
    private readonly IAssistantClientService _assistantClientService;
    private readonly IRepository<Document> _documentRepository;
    private readonly IUserContextService _userContextService;

    public DocumentRetrievalService(
        IRepository<UserDocument> userDocumentRepository,
        IAssistantClientService assistantClientService,
        IRepository<Document> documentRepository,
        IUserContextService userContextService)
    {
        _userDocumentRepository = userDocumentRepository;
        _assistantClientService = assistantClientService;
        _documentRepository = documentRepository;
        _userContextService = userContextService;
    }

    public async Task<UserDocument> GetDocumentDetailsById(Guid documentId)
    {
        var userDocument = await _userDocumentRepository.GetSingleOrDefault(
            new UserDocumentRetrievalByIdSpecification(documentId, _userContextService.UserId));

        if (userDocument != null)
        {
            return userDocument;
        }

        var document = await _documentRepository.Single(new DocumentRetrievalByIdSpecification(documentId));
        userDocument = new UserDocument
        {
            ThreadId = (await _assistantClientService.CreateThread()).Id,
            CurrentLine = 0,
            UserId = _userContextService.UserId,
            DocumentId = documentId,
            Document = document,
            SessionId = Guid.NewGuid(),
            IsCompleted = false,
        };

        await _userDocumentRepository.Add(userDocument);

        return userDocument;
    }
}