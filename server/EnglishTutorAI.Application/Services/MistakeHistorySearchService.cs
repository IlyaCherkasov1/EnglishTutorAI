using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Translates;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class MistakeHistorySearchService : IMistakeHistorySearchService
{
    private readonly IRepository<LinguaFixMessage> _linguaFixMessageRepository;
    private readonly IUserContextService _userContextService;

    public MistakeHistorySearchService(
        IRepository<LinguaFixMessage> linguaFixMessageRepository,
        IUserContextService userContextService)
    {
        _linguaFixMessageRepository = linguaFixMessageRepository;
        _userContextService = userContextService;
    }

    public async Task<SearchResult<MistakeHistoryItems>> Search(PaginationSearchModel model)
    {
        return await _linguaFixMessageRepository.Search(
            new MistakeHistoryItemsSpecification(model, _userContextService.UserId));
    }
}