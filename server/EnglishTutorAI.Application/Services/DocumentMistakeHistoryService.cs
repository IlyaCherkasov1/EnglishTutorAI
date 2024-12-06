using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.Documents;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DocumentMistakeHistoryService : IDocumentMistakeHistoryService
{
    private readonly IRepository<LinguaFixMessage> _linguaFixMessageRepository;
    private readonly IRepository<UserDocument> _userDocumentRepository;

    public DocumentMistakeHistoryService(
        IRepository<LinguaFixMessage> linguaFixMessageRepository,
        IRepository<UserDocument> userDocumentRepository)
    {
        _linguaFixMessageRepository = linguaFixMessageRepository;
        _userDocumentRepository = userDocumentRepository;
    }

    public async Task<IEnumerable<DocumentMistakeHistoryItems>> Get(Guid userDocumentId)
    {
        var sessionId = await _userDocumentRepository.Single(new SessionIdByUserDocumentSpecification(userDocumentId));

        return await _linguaFixMessageRepository.List(new DocumentMistakeHistoryItemsSpecification(sessionId));
    }
}