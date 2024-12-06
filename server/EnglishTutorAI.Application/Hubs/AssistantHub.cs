using Microsoft.AspNetCore.SignalR;

namespace EnglishTutorAI.Application.Hubs;

public class AssistantHub : Hub
{
    public async Task JoinAssistantChat(string threadId, Guid userDocumentId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"{userDocumentId}-{threadId}-Assistant");
    }

    public async Task JoinExplanationChat(string threadId, Guid userDocumentId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"{userDocumentId}-{threadId}-Explanation");
    }
}