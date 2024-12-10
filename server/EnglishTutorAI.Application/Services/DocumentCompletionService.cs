using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.Common;
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
    private readonly IRepository<Document> _documentRepository;

    public DocumentCompletionService(
        IUserAchievementService userAchievementService,
        IRepository<UserDocumentCompletion> userDocumentCompletionRepository,
        IRepository<UserDocument> userDocumentRepository,
        IUserContextService userContextService, IRepository<Document> documentRepository)
    {
        _userAchievementService = userAchievementService;
        _userDocumentCompletionRepository = userDocumentCompletionRepository;
        _userDocumentRepository = userDocumentRepository;
        _userContextService = userContextService;
        _documentRepository = documentRepository;
    }

    public async Task Save(Guid userDocumentId)
    {
        var userId = _userContextService.UserId;
        var isDocumentCompleted = await _userDocumentCompletionRepository.Any(
                new UserDocumentCompletionForAchievementsSpecification(userId, userDocumentId));

        var userDocument = await _userDocumentRepository.GetById(userDocumentId);
        userDocument.IsCompleted = true;
        userDocument.CompletedOn = DateTime.UtcNow;

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

    public async Task<SearchResult<CompletedDocumentListItem>> GetCompletedDocuments(PaginationSearchModel model)
    {
        return await _documentRepository.Search(
            new CompletedUserDocumentsSpecification(_userContextService.UserId, model));
    }
}