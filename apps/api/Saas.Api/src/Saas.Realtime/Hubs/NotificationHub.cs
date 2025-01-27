using Microsoft.AspNetCore.SignalR;
using Saas.Application.Common.Events;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces;
using Saas.Realtime.Clients;

namespace Saas.Realtime.Hubs;

public class NotificationHub : Hub<INotificationClient>
{
    public NotificationHub(IEventService eventService)
    {
        eventService.Subscribe<ChatMessageSentEvent>(async e =>
        {
            var notification = new Notification(NotificationType.ChatMessage, e.Message);
            
            await Clients
                .GroupExcept(e.ChatId.ToString(), e.SenderConnectionId)
                .GetNotification(notification.ToDto());
        });
    }
}