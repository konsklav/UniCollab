using Saas.Application.Common.Notifications;
using Saas.Application.Contracts;

namespace Saas.Realtime;

public static class NotificationExtensions
{
    public static NotificationDto ToDto(this Notification notification) => 
        new(Type: notification.Type.ToString(), 
            Message: notification.Message);
}