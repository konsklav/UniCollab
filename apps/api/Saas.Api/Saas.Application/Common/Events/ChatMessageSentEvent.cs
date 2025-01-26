namespace Saas.Application.Common.Events;

public sealed record ChatMessageSentEvent(
    string SenderUsername,
    Guid SenderId,
    string SenderConnectionId,
    Guid ChatId,
    string Message,
    DateTime SentAt);