using AutoMapper;
using EnglishTutorAI.Application.Interfaces;
using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Domain.Enums;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetConversationThread;

public class GetConversationThreadCommandHandler
    : IRequestHandler<GetConversationThreadCommand, IEnumerable<ChatMessageResponse>>
{
    private readonly IAssistantClientService _assistantClientService;
    private readonly IMapper _mapper;

    public GetConversationThreadCommandHandler(IAssistantClientService assistantClientService, IMapper mapper)
    {
        _assistantClientService = assistantClientService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ChatMessageResponse>> Handle(
        GetConversationThreadCommand request,
        CancellationToken cancellationToken)
    {
        var result = await _assistantClientService.GetAllMessages(request.ThreadId);

        return _mapper.Map<IEnumerable<ChatMessageResponse>>(result);
    }
}