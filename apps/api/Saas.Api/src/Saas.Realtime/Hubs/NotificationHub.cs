using Microsoft.AspNetCore.SignalR;
using Saas.Application.Common.Events;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Realtime;

namespace Saas.Realtime.Hubs;

public class NotificationHub : Hub<INotificationClient>
{
    public NotificationHub(IEventService eventService)
    {
        eventService.Subscribe<ChatMessageSentEvent>(async e =>
        {
            var notification = new Notification(NotificationType.ChatMessage, e.Message.Content);
            
            await Clients
                .GroupExcept(e.ChatId.ToString(), e.SenderConnectionId)
                .GetNotification(notification.ToDto());
        });
    }
}