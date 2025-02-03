namespace Saas.Application.Common.Notifications;

public sealed record Notification(NotificationType Type, string Header, string Message, object[] Metadata);