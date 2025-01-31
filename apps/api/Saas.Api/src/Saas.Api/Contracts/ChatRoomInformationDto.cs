using System.Collections.Concurrent;
using Saas.Domain;

namespace Saas.Api.Contracts;

public sealed record ChatRoomInformationDto(
    Guid Id,
    string Name,
    string? LastMessage,
    int ParticipantCount)
{
    internal static ChatRoomInformationDto From(ChatRoom room) =>
        new(Id: room.Id,
            Name: room.Name.Value,
            ParticipantCount: room.Participants.Count,
            LastMessage: room.Messages.Count == 0 ? null : room.Messages.MaxBy(m => m.SentAt)?.Content);
}