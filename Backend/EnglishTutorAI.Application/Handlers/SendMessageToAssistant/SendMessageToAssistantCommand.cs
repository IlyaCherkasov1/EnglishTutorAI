using EnglishTutorAI.Application.Models;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.SendMessageToAssistant;

public class SendMessageToAssistantCommand : IRequest<string>
{
    public SendMessageToAssistantCommand(SendMessageRequest sendMessageRequest)
    {
        SendMessageRequest = sendMessageRequest;
    }

    public SendMessageRequest SendMessageRequest { get; }
}