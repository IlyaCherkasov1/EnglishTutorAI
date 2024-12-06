using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class StatisticsService : IStatisticsService
{
    private readonly IRepository<LinguaFixMessage> _linguaFixMessageRepository;
    private readonly IRepository<UserDocument> _userDocumentRepository;
    private readonly IUserStatisticsService _userStatisticsService;

    public StatisticsService(
        IRepository<LinguaFixMessage> linguaFixMessageRepository,
        IRepository<UserDocument> userDocumentRepository,
        IUserStatisticsService userStatisticsService)
    {
        _linguaFixMessageRepository = linguaFixMessageRepository;
        _userDocumentRepository = userDocumentRepository;
        _userStatisticsService = userStatisticsService;
    }


    public async Task SaveStatisticsAndMessage(SaveStatisticsAndMessageModel model)
    {
        await _userStatisticsService.UpdateStatisticsAndSaveMessage(model.TranslatedText, model.CorrectedText);
        var sessionId = await _userDocumentRepository.Single(new SessionIdByUserDocumentSpecification(model.UserDocumentId));

        await _linguaFixMessageRepository.Add(new LinguaFixMessage
        {
            TranslatedText = model.TranslatedText,
            CorrectedText = model.CorrectedText,
            UserDocumentId = model.UserDocumentId,
            SessionId = sessionId,
        });
    }
}