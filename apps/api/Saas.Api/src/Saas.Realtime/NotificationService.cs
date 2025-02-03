using Microsoft.AspNetCore.SignalR;
using Saas.Application.Common.Events;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Realtime;
using Saas.Realtime.Hubs;

namespace Saas.Realtime;

internal sealed class NotificationService(
    IEventService eventService,
    IHubContext<NotificationHub, INotificationClient> notificationHub) : INotificationService
{
    public async Task SendAsync(Notification notification)
    {
        if (notification.Type is NotificationType.PostUploaded)
        {
            await notificationHub.Clients.All.GetNotification(notification.ToDto());
        }
    }

    public void InitializeSubscriptions()
    {
        eventService.Subscribe<ChatMessageSentEvent>(async e =>
        {
            var notification = new Notification(
                Type: NotificationType.ChatMessage,
                Header: $"{e.Message.Sender.Username} sent a message",
                Message: e.Message.Content,
                Metadata: [
                    new { e.ChatId }
                ]);

            var chatConnections = ChatHub.UserConnections.GetValueOrDefault(e.ChatId, [])
                .Select(info => NotificationHub.UserConnections.GetValueOrDefault(info.UserId, string.Empty))
                .Where(s => !string.IsNullOrEmpty(s));
            
            await notificationHub
                .Clients
                .GroupExcept(e.ChatId.ToString(), chatConnections)
                .GetNotification(notification.ToDto());
        });
    }
}