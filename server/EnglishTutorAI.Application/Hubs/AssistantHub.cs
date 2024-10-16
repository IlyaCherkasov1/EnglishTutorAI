using Microsoft.AspNetCore.SignalR;

namespace EnglishTutorAI.Application.Hubs;

public class AssistantHub : Hub
{
    public async Task JoinChat(string threadId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, threadId);
    }
}