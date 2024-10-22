using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.SendMessageToAssistantWithSave;

public class SendMessageToAssistantWithSaveCommand : IRequest<string>
{
    public SendMessageToAssistantWithSaveCommand(SendMessageRequest sendMessageRequest)
    {
        SendMessageRequest = sendMessageRequest;
    }

    public SendMessageRequest SendMessageRequest { get; }
}