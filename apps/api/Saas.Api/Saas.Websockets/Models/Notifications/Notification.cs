using Saas.Websockets.Hubs;

namespace Saas.Websockets.Models.Notifications;

public sealed record Notification(NotificationType Type, string Message)
{
    public NotificationDto ToDto() => new(Type: Type.ToString(), Message: Message);
}