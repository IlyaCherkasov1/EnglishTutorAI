using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Models.Translates;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TranslateSearchService : ITranslateSearchService
{
    private readonly IRepository<Translate> _translateRepository;
    private readonly IUserContextService _userContextService;

    public TranslateSearchService(
        IRepository<Translate> translateRepository,
        IUserContextService userContextService)
    {
        _translateRepository = translateRepository;
        _userContextService = userContextService;
    }

    public async Task<SearchResult<TranslateListItem>> Search(TranslateSearchModel model)
    {
        return await _translateRepository.Search(
            new TranslateListSearchSpecification(model, _userContextService.UserId));
    }

    public async Task<TranslateListItem?> GetNextTranslate(NextTranslateSearchModel model)
    {
        return await _translateRepository.GetFirstOrDefault(
            new NextTranslateSearchSpecification(model, _userContextService.UserId));
    }
}