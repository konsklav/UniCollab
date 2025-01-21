using Microsoft.AspNetCore.SignalR;
using Saas.Application.Common.Events;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces;
using Saas.Websockets.Contracts;

namespace Saas.Websockets.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(ClientMessage message);
}

public record ServerMessage(string ChatId, string UserId, string Content);
public record ClientMessage(string Username, string Content);

public class ChatHub(
    IHubContext<NotificationHub, INotificationClient> notificationHub,
    IEventService eventService) 
    : Hub<IChatClient>
{
    public async Task JoinChat(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }

    public async Task LeaveChat(string chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
    }
    
    public async Task SendMessage(ServerMessage message)
    {
        var username = Context.User?
            .FindFirst(c => c.Type.Equals("Username", StringComparison.OrdinalIgnoreCase))?.Value ?? Context.ConnectionId;

        // var chatGuid = Guid.Parse(message.ChatId);
        // var userGuid = Guid.Parse(message.UserId);
        //
        // var messageSentEvent = new ChatMessageSentEvent(
        //     UserId: userGuid,
        //     ChatId: chatGuid,
        //     Message: message.Content,
        //     SentAt: DateTime.UtcNow);

        // await eventService.PublishAsync(messageSentEvent);
        
        await Clients.Group(message.ChatId).ReceiveMessage(new ClientMessage(username, message.Content));

        var notification = new Notification(NotificationType.ChatMessage, message.Content);
        
        await notificationHub.Clients
            .GroupExcept(message.ChatId, Context.ConnectionId)
            .GetNotification(notification.ToDto());
    }
}