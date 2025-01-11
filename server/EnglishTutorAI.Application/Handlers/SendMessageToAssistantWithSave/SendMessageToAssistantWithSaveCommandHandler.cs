using EnglishTutorAI.Application.Interfaces;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.SendMessageToAssistantWithSave;

public class SendMessageWithSaveCommandHandler : IRequestHandler<SendMessageToAssistantWithSaveCommand, string>
{
    private readonly ISendAssistantMessageService _sendAssistantMessageService;
    private readonly IUnitOfWork _unitOfWork;

    public SendMessageWithSaveCommandHandler(ISendAssistantMessageService sendAssistantMessageService, IUnitOfWork unitOfWork)
    {
        _sendAssistantMessageService = sendAssistantMessageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(SendMessageToAssistantWithSaveCommand request, CancellationToken cancellationToken)
    {
        var result = await _sendAssistantMessageService.SendMessageAndSaveToTheRepository(request.SendMessageRequest);
        await _unitOfWork.Commit();

        return result;
    }
}