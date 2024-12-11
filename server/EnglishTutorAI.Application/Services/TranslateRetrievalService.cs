using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TranslateRetrievalService : ITranslateRetrievalService
{
    private readonly IRepository<UserTranslate> _userTranslateRepository;
    private readonly IAssistantClientService _assistantClientService;
    private readonly IRepository<Translate> _translateRepository;
    private readonly IUserContextService _userContextService;

    public TranslateRetrievalService(
        IRepository<UserTranslate> userTranslateRepository,
        IAssistantClientService assistantClientService,
        IRepository<Translate> translateRepository,
        IUserContextService userContextService)
    {
        _userTranslateRepository = userTranslateRepository;
        _assistantClientService = assistantClientService;
        _translateRepository = translateRepository;
        _userContextService = userContextService;
    }

    public async Task<UserTranslate> GetTranslateDetailsById(Guid translateId)
    {
        var userTranslate = await _userTranslateRepository.GetSingleOrDefault(
            new UserTranslateRetrievalByIdSpecification(translateId, _userContextService.UserId));

        if (userTranslate != null)
        {
            return userTranslate;
        }

        var translate = await _translateRepository.Single(new TranslateRetrievalByIdSpecification(translateId));
        userTranslate = new UserTranslate
        {
            ThreadId = (await _assistantClientService.CreateThread()).Id,
            CurrentLine = 0,
            UserId = _userContextService.UserId,
            TranslateId = translateId,
            Translate = translate,
            SessionId = Guid.NewGuid(),
            IsCompleted = false,
        };

        await _userTranslateRepository.Add(userTranslate);

        return userTranslate;
    }
}