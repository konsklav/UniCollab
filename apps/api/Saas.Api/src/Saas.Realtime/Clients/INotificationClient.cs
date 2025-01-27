using Saas.Realtime.Contracts;

namespace Saas.Realtime.Clients;

public interface INotificationClient
{
    Task GetNotification(NotificationDto notification);
}