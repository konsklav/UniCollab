using Microsoft.AspNetCore.SignalR;
using Saas.Application.Common.Events;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces;
using Saas.Websockets.Contracts;

namespace Saas.Websockets.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string username, string message);
}

internal sealed class ChatHub(
    IHubContext<NotificationHub, INotificationClient> notificationHub,
    IEventService eventService) 
    : Hub<IChatClient>
{
    public async Task JoinChat(Guid chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }

    public async Task LeaveChat(Guid chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
    }
    
    public async Task SendMessage(Guid chatId, Guid userId, string message)
    {
        var username = Context.User?.FindFirst(c => c.Type == "Username")?.Value ?? Context.ConnectionId;
        
        var messageSentEvent = new ChatMessageSentEvent(
            UserId: userId,
            ChatId: chatId,
            Message: message,
            SentAt: DateTime.UtcNow);

        // await eventService.PublishAsync(messageSentEvent);
        
        await Clients.Group(chatId.ToString()).ReceiveMessage(username, message);

        var notification = new Notification(NotificationType.ChatMessage, message);
        
        await notificationHub.Clients
            .GroupExcept(chatId.ToString(), Context.ConnectionId)
            .GetNotification(notification.ToDto());
    }
}