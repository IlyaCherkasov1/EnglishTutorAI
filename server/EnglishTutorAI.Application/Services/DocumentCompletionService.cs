using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class DocumentCompletionService : IDocumentCompletionService
{
    private readonly IAuthenticatedUserContext _authenticatedUserContext;
    private readonly IUserAchievementService _userAchievementService;
    private readonly IRepository<UserDocumentCompletion> _userDocumentCompletionRepository;

    public DocumentCompletionService(
        IAuthenticatedUserContext authenticatedUserContext,
        IUserAchievementService userAchievementService,
        IRepository<UserDocumentCompletion> userDocumentCompletionRepository)
    {
        _authenticatedUserContext = authenticatedUserContext;
        _userAchievementService = userAchievementService;
        _userDocumentCompletionRepository = userDocumentCompletionRepository;
    }

    public async Task Save(Guid documentId)
    {
        var userId = _authenticatedUserContext.UserId!.Value;
        var isDocumentCompleted = await _userDocumentCompletionRepository.Any(
                new UserDocumentCompletionForAchievementsSpecification(userId, documentId));

        if (!isDocumentCompleted)
        {
            await _userDocumentCompletionRepository.Add(new UserDocumentCompletion
            {
                UserId = userId,
                DocumentId = documentId,
            });

            await _userAchievementService.UpdateProgress(userId, AchievementIds.DedicatedTranslatorId);
        }
    }
}