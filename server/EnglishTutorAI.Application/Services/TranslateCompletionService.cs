using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TranslateCompletionService : ITranslateCompletionService
{
    private readonly IUserAchievementService _userAchievementService;
    private readonly IRepository<UserTranslateCompletion> _userTranslateCompletionRepository;
    private readonly IRepository<UserTranslate> _userTranslateRepository;
    private readonly IUserContextService _userContextService;
    private readonly IRepository<Translate> _translateRepository;

    public TranslateCompletionService(
        IUserAchievementService userAchievementService,
        IRepository<UserTranslateCompletion> userTranslateCompletionRepository,
        IRepository<UserTranslate> userTranslateRepository,
        IUserContextService userContextService, IRepository<Translate> translateRepository)
    {
        _userAchievementService = userAchievementService;
        _userTranslateCompletionRepository = userTranslateCompletionRepository;
        _userTranslateRepository = userTranslateRepository;
        _userContextService = userContextService;
        _translateRepository = translateRepository;
    }

    public async Task Save(Guid userTranslateId)
    {
        var userId = _userContextService.UserId;
        var isTranslateCompleted = await _userTranslateCompletionRepository.Any(
                new UserTranslateCompletionForAchievementsSpecification(userId, userTranslateId));

        var userTranslate = await _userTranslateRepository.GetById(userTranslateId);
        userTranslate.IsCompleted = true;
        userTranslate.CompletedOn = DateTime.UtcNow;

        if (!isTranslateCompleted)
        {
            await _userTranslateCompletionRepository.Add(new UserTranslateCompletion
            {
                UserId = userId,
                UserTranslateId = userTranslateId,
            });

            await _userAchievementService.UpdateProgress(userId, AchievementIds.DedicatedTranslatorId);
        }
    }

    public async Task<SearchResult<CompletedTranslateListItem>> GetCompletedTranslates(PaginationSearchModel model)
    {
        return await _translateRepository.Search(
            new CompletedUserTranslatesSpecification(_userContextService.UserId, model));
    }
}