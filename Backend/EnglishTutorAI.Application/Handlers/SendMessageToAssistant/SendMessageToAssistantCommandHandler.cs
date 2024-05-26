using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.SendMessageToAssistant;

public class SendMessageToAssistantCommandHandler : IRequestHandler<SendMessageToAssistantCommand, string>
{
    private readonly ISendAssistantMessageService _sendAssistantMessageService;

    public SendMessageToAssistantCommandHandler(ISendAssistantMessageService sendAssistantMessageService)
    {
        _sendAssistantMessageService = sendAssistantMessageService;
    }

    public Task<string> Handle(SendMessageToAssistantCommand request, CancellationToken cancellationToken)
    {
        return _sendAssistantMessageService.SendMessageAndRun(request.SendMessageRequest);
    }
}