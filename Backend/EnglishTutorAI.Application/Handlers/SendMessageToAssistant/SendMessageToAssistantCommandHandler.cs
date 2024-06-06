using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.SendMessageToAssistant;

public class SendMessageToAssistantCommandHandler : IRequestHandler<SendMessageToAssistantCommand, string>
{
    private readonly ISendAssistantMessageService _sendAssistantMessageService;
    private readonly IUnitOfWork _unitOfWork;

    public SendMessageToAssistantCommandHandler(
        ISendAssistantMessageService sendAssistantMessageService,
        IUnitOfWork unitOfWork)
    {
        _sendAssistantMessageService = sendAssistantMessageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(SendMessageToAssistantCommand request, CancellationToken cancellationToken)
    {
        var result = await _sendAssistantMessageService.SendMessageAndRun(request.SendMessageRequest);
        await _unitOfWork.Commit();

        return result;
    }
}