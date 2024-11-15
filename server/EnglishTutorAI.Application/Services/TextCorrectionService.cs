using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.TextGeneration;
using EnglishTutorAI.Domain.Entities;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TextCorrectionService : ITextCorrectionService
{
    private readonly string _assistantId;
    private readonly IAssistantClientService _assistantClientService;
    private readonly ITextCorrectionMessageGenerationService _messageGenerationService;
    private readonly ISingleEntryCache _singleEntryCache;
    private readonly IRepository<LinguaFixMessage> _linguaFixMessageRepository;
    private readonly IAuthenticatedUserContext _authenticatedUserContext;
    private readonly ITextProcessingService _textProcessingService;
    private readonly IUserManagementService _userManagementService;
    private Guid UserId => _authenticatedUserContext.UserId!.Value;

    public TextCorrectionService(
        IAssistantClientService assistantClientService,
        IOptionsMonitor<OpenAiConfig> openAiConfig,
        ITextCorrectionMessageGenerationService messageGenerationService,
        ISingleEntryCache singleEntryCache,
        IRepository<LinguaFixMessage> linguaFixMessageRepository,
        IAuthenticatedUserContext authenticatedUserContext,
        ITextProcessingService textProcessingService,
        IUserManagementService userManagementService)
    {
        _assistantClientService = assistantClientService;
        _messageGenerationService = messageGenerationService;
        _singleEntryCache = singleEntryCache;
        _linguaFixMessageRepository = linguaFixMessageRepository;
        _authenticatedUserContext = authenticatedUserContext;
        _textProcessingService = textProcessingService;
        _userManagementService = userManagementService;
        _assistantId = openAiConfig.CurrentValue.EnglishFixerAssistantId!;
    }

    public async Task<TextCorrectionResult> Correct(TextGenerationRequest request)
    {
        var correctedText = await GetOrGenerateCorrectedText(request);
        var isCorrected = _textProcessingService.HasTextChanged(request.TranslatedText, correctedText);

        if (isCorrected)
        {
            await UpdateStatisticsAndSaveMessage(request, correctedText);
        }
        else
        {
            await _userManagementService.UpdateAchievement(UserId, AchievementIds.NoviceTranslatorId);
        }

        return new TextCorrectionResult(correctedText, isCorrected);
    }

    private async Task<string> GetOrGenerateCorrectedText(TextGenerationRequest request)
    {
        var cachedText = _singleEntryCache.Get(request.OriginalText);

        if (cachedText != null)
        {
            return cachedText;
        }

        await GenerateAndAddUserMessageAsync(request);
        await _assistantClientService.CreateRunRequest(_assistantId, request.ThreadId);
        var correctedText = await GenerateCorrectedMessageAsync(request);

        var isCorrected = _textProcessingService.HasTextChanged(request.TranslatedText, correctedText);

        if (!isCorrected)
        {
            await _userManagementService.UpdateAchievement(UserId, AchievementIds.FlawlessTranslatorId);
        }

        _singleEntryCache.Set(request.OriginalText, correctedText);

        return correctedText;
    }

    private async Task UpdateStatisticsAndSaveMessage(TextGenerationRequest request, string correctedText)
    {
        var countMistakes = _textProcessingService.CountErrors(request.TranslatedText, correctedText);
        await _userManagementService.UpdateStatistics(UserId, countMistakes);

        await _linguaFixMessageRepository.Add(new LinguaFixMessage
        {
            ThreadId = request.ThreadId,
            TranslatedText = request.TranslatedText,
            CorrectedText = correctedText,
            DocumentId = request.DocumentId,
            DocumentSessionId = request.SessionId
        });
    }

    private async Task GenerateAndAddUserMessageAsync(TextGenerationRequest request)
    {
        var message = await _messageGenerationService.GenerateMessageAsync(request);
        await _assistantClientService.AddMessageToThread(request.ThreadId, message);
    }

    private async Task<string> GenerateCorrectedMessageAsync(TextGenerationRequest request)
    {
        var correctedText = await _assistantClientService.GetLastMessage(request.ThreadId);
        var cleanCorrectedText = _textProcessingService.ExtractCleanText(correctedText, request.OriginalText);

        return cleanCorrectedText;
    }
}