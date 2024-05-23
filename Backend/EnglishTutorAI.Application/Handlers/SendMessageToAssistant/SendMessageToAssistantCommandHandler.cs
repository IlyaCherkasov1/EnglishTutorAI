using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.SendMessageToAssistant;

public class SendMessageToAssistantCommandHandler : IRequestHandler<SendMessageToAssistantCommand, string>
{
    private readonly IAssistantService _assistantService;

    public SendMessageToAssistantCommandHandler(IAssistantService assistantService)
    {
        _assistantService = assistantService;
    }

    public Task<string> Handle(SendMessageToAssistantCommand request, CancellationToken cancellationToken)
    {
        return _assistantService.StartConversation(request.SendMessageRequest.Message);
    }
}