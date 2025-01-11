using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class SaveCurrentLineService : ISaveCurrentLineService
{
    private readonly IRepository<UserTranslate> _userTranslateRepository;

    public SaveCurrentLineService(IRepository<UserTranslate> userTranslateRepository)
    {
        _userTranslateRepository = userTranslateRepository;
    }

    public async Task SaveCurrentLine(SaveCurrentLineRequest request)
    {
        var userTranslate = await _userTranslateRepository.GetById(request.UserTranslateId);
        userTranslate.CurrentLine = request.CurrentLine;
    }
}