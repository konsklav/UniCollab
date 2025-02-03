using Saas.Application.Common.Notifications;

namespace Saas.Application.Interfaces;

/// <summary>
/// Allows other application services and use cases to send notifications using the Realtime API.
/// </summary>
public interface INotificationService
{
    Task SendAsync(Notification notification);
}