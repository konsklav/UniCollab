using Saas.Application.Common.Notifications;

namespace Saas.Websockets.Contracts;

public static class NotificationExtensions
{
    public static NotificationDto ToDto(this Notification notification) => 
        new(Type: notification.Type.ToString(), 
            Message: notification.Message);
}