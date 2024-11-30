using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Documents;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class MistakeHistorySearchService : IMistakeHistorySearchService
{
    private readonly IRepository<LinguaFixMessage> _linguaFixMessageRepository;

    public MistakeHistorySearchService(IRepository<LinguaFixMessage> linguaFixMessageRepository)
    {
        _linguaFixMessageRepository = linguaFixMessageRepository;
    }

    public async Task<SearchResult<MistakeHistoryItems>> Search(PaginationSearchModel model)
    {
        return await _linguaFixMessageRepository.Search(new MistakeHistoryItemsSpecification(model));
    }
}