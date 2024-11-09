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

    public DocumentMistakeHistoryService(IRepository<LinguaFixMessage> linguaFixMessageRepository)
    {
        _linguaFixMessageRepository = linguaFixMessageRepository;
    }

    public async Task<IEnumerable<DocumentMistakeHistoryItems>> Get(Guid sessionId)
    {
        return await _linguaFixMessageRepository.List(new DocumentMistakeHistoryItemsSpecification(sessionId));
    }
}