using Microsoft.AspNetCore.SignalR;
using Saas.Websockets.Models.Notifications;

namespace Saas.Websockets.Hubs;

internal sealed class NotificationHub : Hub<INotificationClient>;

internal interface INotificationClient
{
    Task GetNotification(NotificationDto notification);
}