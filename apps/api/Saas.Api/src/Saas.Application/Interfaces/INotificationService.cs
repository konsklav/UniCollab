using Saas.Application.Common.Notifications;

namespace Saas.Application.Interfaces;

public interface INotificationService
{
    Task SendAsync(Notification notification);
}