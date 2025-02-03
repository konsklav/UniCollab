using Microsoft.AspNetCore.SignalR;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Realtime;
using Saas.Realtime.Hubs;

namespace Saas.Realtime;

internal sealed class NotificationService(IHubContext<NotificationHub, INotificationClient> notificationHub) 
    : INotificationService
{
    public async Task SendAsync(Notification notification)
    {
        if (notification.Type is NotificationType.PostUploaded)
        {
            await notificationHub.Clients.All.GetNotification(notification.ToDto());
        }
    }
}