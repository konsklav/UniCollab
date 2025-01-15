using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;

namespace Saas.Websockets.Hubs;

internal interface IChatClient
{
    Task ReceiveMessage(string username, string message);
}

internal sealed class ChatHub(IHubContext<NotificationHub, INotificationClient> notificationHub) : Hub<IChatClient>
{
    public async Task JoinChat(Guid chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }

    public async Task LeaveChat(Guid chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
    }
    
    public async Task SendMessage(Guid chatId, string username, string message)
    {
        await Clients.Group(chatId.ToString()).ReceiveMessage(username, message);
        notificationHub.Clients.GroupExcept(chatId.ToString(), Context.ConnectionId);
    }
}