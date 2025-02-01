using Saas.Domain;

namespace Saas.Application.Common.Events;

public sealed record ChatMessageSentEvent(
    string SenderConnectionId,
    Guid ChatId,
    Message Message);