using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TranslateDeletionService : ITranslateDeletionService
{
    private readonly IRepository<Translate> _translateRepository;

    public TranslateDeletionService(IRepository<Translate> translateRepository)
    {
        _translateRepository = translateRepository;
    }

    public async Task Delete(Guid translateId)
    {
        var translate = await _translateRepository.GetById(translateId);
        _translateRepository.Delete(translate);
    }
}