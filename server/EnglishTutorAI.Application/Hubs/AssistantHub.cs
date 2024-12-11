using Microsoft.AspNetCore.SignalR;

namespace EnglishTutorAI.Application.Hubs;

public class AssistantHub : Hub
{
    public async Task JoinAssistantChat(string threadId, Guid userTranslateId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"{userTranslateId}-{threadId}-Assistant");
    }

    public async Task JoinExplanationChat(string threadId, Guid userTranslateId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"{userTranslateId}-{threadId}-Explanation");
    }
}