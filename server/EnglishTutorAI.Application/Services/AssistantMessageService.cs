using EnglishTutorAI.Application.Attributes;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models.TextGeneration;

namespace EnglishTutorAI.Application.Services;

[ScopedDependency]
public class AssistantMessageService : IAssistantMessageService
{
    private readonly IAssistantClientService _assistantClientService;
    private readonly ITextCorrectionMessageGenerationService _messageGenerationService;
    private readonly ITextProcessingService _textProcessingService;

    public AssistantMessageService(
        IAssistantClientService assistantClientService,
        ITextCorrectionMessageGenerationService messageGenerationService,
        ITextProcessingService textProcessingService)
    {
        _assistantClientService = assistantClientService;
        _messageGenerationService = messageGenerationService;
        _textProcessingService = textProcessingService;
    }

    public async Task GenerateAndAddMessageAsync(TextGenerationRequest request, string threadId)
    {
        var message = await _messageGenerationService.GenerateMessageAsync(request);
        await _assistantClientService.AddMessageToThread(threadId, message);
    }

    public async Task<string> GetCorrectedMessageAsync(string originalText, string threadId)
    {
        var correctedText = await _assistantClientService.GetLastMessage(threadId);
        return _textProcessingService.ExtractCleanText(correctedText, originalText);
    }
}