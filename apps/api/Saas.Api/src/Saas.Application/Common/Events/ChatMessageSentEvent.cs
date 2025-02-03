using Saas.Domain;

namespace Saas.Application.Common.Events;

public sealed record ChatMessageSentEvent(
    Guid SenderId,
    Guid ChatId,
    Message Message);