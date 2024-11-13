using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Application.Models.TextGeneration;
using EnglishTutorAI.Application.Specifications;
using EnglishTutorAI.Domain.Entities;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services
{
    [ScopedDependency]
    public class TextCorrectionService : ITextCorrectionService
    {
        private readonly string _assistantId;
        private readonly IAssistantClientService _assistantClientService;
        private readonly ITextComparisonService _textComparisonService;
        private readonly ITextExtractionService _textExtractionService;
        private readonly ITextCorrectionMessageGenerationService _messageGenerationService;
        private readonly ISingleEntryCache _singleEntryCache;
        private readonly IRepository<LinguaFixMessage> _linguaFixMessageRepository;
        private readonly IRepository<UserStatistics> _userStatisticsRepository;
        private readonly ITextErrorDetectionService _textErrorDetectionService;
        private readonly IAuthenticatedUserContext _authenticatedUserContext;

        public TextCorrectionService(
            IAssistantClientService assistantClientService,
            IOptionsMonitor<OpenAiConfig> openAiConfig,
            ITextComparisonService textComparisonService,
            ITextExtractionService textExtractionService,
            ITextCorrectionMessageGenerationService messageGenerationService,
            ISingleEntryCache singleEntryCache,
            IRepository<LinguaFixMessage> linguaFixMessageRepository,
            IRepository<UserStatistics> userStatisticsRepository,
            ITextErrorDetectionService textErrorDetectionService,
            IAuthenticatedUserContext authenticatedUserContext)
        {
            _assistantClientService = assistantClientService;
            _textComparisonService = textComparisonService;
            _textExtractionService = textExtractionService;
            _messageGenerationService = messageGenerationService;
            _singleEntryCache = singleEntryCache;
            _linguaFixMessageRepository = linguaFixMessageRepository;
            _userStatisticsRepository = userStatisticsRepository;
            _textErrorDetectionService = textErrorDetectionService;
            _authenticatedUserContext = authenticatedUserContext;
            _assistantId = openAiConfig.CurrentValue.EnglishFixerAssistantId!;
        }

        public async Task<TextCorrectionResult> Correct(TextGenerationRequest request)
        {
            var correctedText = await GetOrGenerateCorrectedText(request);
            var isCorrected = _textComparisonService.HasTextChanged(request.TranslatedText, correctedText);

            if (isCorrected)
            {
                await UpdateStatisticsAndSaveMessage(request, correctedText);
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

            _singleEntryCache.Set(request.OriginalText, correctedText);

            return correctedText;
        }

        private async Task UpdateStatisticsAndSaveMessage(TextGenerationRequest request, string correctedText)
        {
            var countMistakes = _textErrorDetectionService.CountGroupedErrors(request.TranslatedText, correctedText);
            var userId = _authenticatedUserContext.UserId!.Value;

            var userStatistics = await _userStatisticsRepository.Single(new UserStatisticsByUserIdSpecification(userId));
            userStatistics.CorrectedMistakes += countMistakes;

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
            var cleanCorrectedText = _textExtractionService.ExtractCleanText(correctedText, request.OriginalText);

            return cleanCorrectedText;
        }
    }
}