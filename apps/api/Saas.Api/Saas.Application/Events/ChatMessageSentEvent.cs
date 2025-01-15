namespace Saas.Application.Events;

public sealed record ChatMessageSentEvent(
    Guid UserId,
    string Message,
    DateTime SentAt);