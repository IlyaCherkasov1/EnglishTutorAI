using EnglishTutorAI.Application.Models;
using EnglishTutorAI.Domain.Entities;
using MediatR;

namespace EnglishTutorAI.Application.Handlers.GetConversationThread;

public class GetConversationThreadCommand : IRequest<IReadOnlyList<ChatMessageResponse>>
{
    public GetConversationThreadCommand(string threadId)
    {
        ThreadId = threadId;
    }

    public string ThreadId { get; }
}