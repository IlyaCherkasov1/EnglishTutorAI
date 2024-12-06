using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DocumentCompletionService : IDocumentCompletionService
{
    private readonly IUserAchievementService _userAchievementService;
    private readonly IRepository<UserDocumentCompletion> _userDocumentCompletionRepository;
    private readonly IRepository<UserDocument> _userDocumentRepository;
    private readonly IUserContextService _userContextService;

    public DocumentCompletionService(
        IUserAchievementService userAchievementService,
        IRepository<UserDocumentCompletion> userDocumentCompletionRepository,
        IRepository<UserDocument> userDocumentRepository,
        IUserContextService userContextService)
    {
        _userAchievementService = userAchievementService;
        _userDocumentCompletionRepository = userDocumentCompletionRepository;
        _userDocumentRepository = userDocumentRepository;
        _userContextService = userContextService;
    }

    public async Task Save(Guid userDocumentId)
    {
        var userId = _userContextService.UserId;
        var isDocumentCompleted = await _userDocumentCompletionRepository.Any(
                new UserDocumentCompletionForAchievementsSpecification(_userContextService.UserId, userDocumentId));

        var userDocument = await _userDocumentRepository.GetById(userDocumentId);
        userDocument.IsCompleted = true;

        if (!isDocumentCompleted)
        {
            await _userDocumentCompletionRepository.Add(new UserDocumentCompletion
            {
                UserId = userId,
                UserDocumentId = userDocumentId,
            });

            await _userAchievementService.UpdateProgress(userId, AchievementIds.DedicatedTranslatorId);
        }
    }
}