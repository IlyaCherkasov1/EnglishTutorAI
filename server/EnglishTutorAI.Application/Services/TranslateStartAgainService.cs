using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TranslateStartAgainService : ITranslateStartAgainService
{
    private readonly IRepository<UserTranslate> _userTranslateRepository;

    public TranslateStartAgainService(IRepository<UserTranslate> userTranslateRepository)
    {
        _userTranslateRepository = userTranslateRepository;
    }

    public async Task StartAgain(Guid userTranslateId)
    {
        var userTranslate = await _userTranslateRepository.GetById(userTranslateId);

        userTranslate.CurrentLine = 0;
        userTranslate.IsCompleted = false;
        userTranslate.SessionId = Guid.NewGuid();
    }
}