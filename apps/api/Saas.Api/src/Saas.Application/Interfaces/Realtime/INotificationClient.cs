using Saas.Application.Contracts;

namespace Saas.Application.Interfaces.Realtime;

public interface INotificationClient
{
    Task GetNotification(NotificationDto notification);
}