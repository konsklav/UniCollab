namespace Saas.Application.Common.Events;

public sealed record ChatMessageSentEvent(
    Guid UserId,
    Guid ChatId,
    string Message,
    DateTime SentAt);