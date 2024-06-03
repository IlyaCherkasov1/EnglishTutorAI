using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetConversationThread;

public class GetConversationThreadCommandHandler
    : IRequestHandler<GetConversationThreadCommand, IReadOnlyList<ChatMessageResponse>>
{
    private readonly IAssistantClient _assistantClient;
    private readonly IMapper _mapper;

    public GetConversationThreadCommandHandler(IAssistantClient assistantClient, IMapper mapper)
    {
        _assistantClient = assistantClient;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ChatMessageResponse>> Handle(
        GetConversationThreadCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _assistantClient.GetAllMessages(request.ThreadId, ChatType.Dialog);

        return _mapper.Map<IReadOnlyList<ChatMessageResponse>>(result);
    }
}