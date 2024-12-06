using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Constants;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.TextGeneration;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class TextCorrectionService : ITextCorrectionService
{
    private readonly string _assistantId;
    private readonly IAssistantClientService _assistantClientService;
    private readonly ISingleEntryCache _singleEntryCache;
    private readonly ITextProcessingService _textProcessingService;
    private readonly IUserAchievementService _userAchievementService;
    private readonly IAssistantMessageService _assistantMessageService;
    private readonly IStatisticsService _statisticsService;
    private readonly IUserContextService _userContextService;

    public TextCorrectionService(
        IAssistantClientService assistantClientService,
        IOptionsMonitor<OpenAiConfig> openAiConfig,
        ISingleEntryCache singleEntryCache,
        ITextProcessingService textProcessingService,
        IUserAchievementService userAchievementService,
        IAssistantMessageService assistantMessageService,
        IStatisticsService statisticsService,
        IUserContextService userContextService)
    {
        _assistantClientService = assistantClientService;
        _singleEntryCache = singleEntryCache;
        _textProcessingService = textProcessingService;
        _userAchievementService = userAchievementService;
        _assistantMessageService = assistantMessageService;
        _statisticsService = statisticsService;
        _userContextService = userContextService;
        _assistantId = openAiConfig.CurrentValue.EnglishFixerAssistantId!;
    }

    public async Task<TextCorrectionResult> Correct(TextGenerationRequest request)
    {
        var correctedText = await GetOrGenerateCorrectedText(request, request.ThreadId);
        var isCorrected = _textProcessingService.HasTextChanged(request.TranslatedText, correctedText);

        if (isCorrected)
        {
            await _statisticsService.SaveStatisticsAndMessage(new SaveStatisticsAndMessageModel(
                request.TranslatedText, correctedText, request.UserDocumentId));
        }
        else
        {
            await _userAchievementService.UpdateProgress(_userContextService.UserId, AchievementIds.NoviceTranslatorId);
        }

        return new TextCorrectionResult(correctedText, isCorrected);
    }

    private async Task<string> GetOrGenerateCorrectedText(TextGenerationRequest request, string threadId)
    {
        var cachedText = _singleEntryCache.Get(request.OriginalText);

        if (cachedText != null)
        {
            return cachedText;
        }

        await _assistantMessageService.GenerateAndAddMessageAsync(request, threadId);
        await _assistantClientService.CreateRunRequest(_assistantId, threadId);
        var correctedText = await _assistantMessageService.GetCorrectedMessageAsync(request.OriginalText, threadId);

        var isCorrected = _textProcessingService.HasTextChanged(request.TranslatedText, correctedText);

        if (!isCorrected)
        {
            await _userAchievementService.UpdateProgress(_userContextService.UserId, AchievementIds.FlawlessTranslatorId);
        }

        _singleEntryCache.Set(request.OriginalText, correctedText);

        return correctedText;
    }
}