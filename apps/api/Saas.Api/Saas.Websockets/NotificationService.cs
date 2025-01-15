using Microsoft.AspNetCore.SignalR;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces;
using Saas.Websockets.Contracts;
using Saas.Websockets.Hubs;

namespace Saas.Websockets;

internal sealed class NotificationService(IHubContext<NotificationHub, INotificationClient> notificationHub) 
    : INotificationService
{
    public async Task SendAsync(Notification notification)
    {
        await notificationHub.Clients.All.GetNotification(notification.ToDto());
    }
}