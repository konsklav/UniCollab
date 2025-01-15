using Microsoft.AspNetCore.SignalR;
using Saas.Application.Common.Notifications;

namespace Saas.Websockets.Hubs;

internal sealed class NotificationHub : Hub<INotificationClient>;

public interface INotificationClient
{
    Task GetNotification(NotificationDto notification);
}