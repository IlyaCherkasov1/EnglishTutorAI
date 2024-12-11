using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TranslateCreationCreationService : ITranslateCreationService
{
    private readonly IRepository<Translate> _translateRepository;
    private readonly ISentenceSplitterService _sentenceSplitterService;

    public TranslateCreationCreationService(
        IRepository<Translate> translateRepository,
        ISentenceSplitterService sentenceSplitterService)
    {
        _translateRepository = translateRepository;
        _sentenceSplitterService = sentenceSplitterService;
    }

    public async Task AddTranslate(TranslateCreationRequest creationRequest)
    {
        var translate = new Translate
        {
            Title = creationRequest.Title,
            StudyTopic = creationRequest.StudyTopic,
        };

        translate.Sentences = _sentenceSplitterService.Split(creationRequest.Content)
            .Select((text, index) => new TranslateSentence
            {
                TranslateId = translate.Id,
                Text = text,
                Position = index + 1
            }).ToList();

        await _translateRepository.Add(translate);
    }
}