using System.Collections.Concurrent;
using Saas.Domain;

namespace Saas.Api.Contracts;

public sealed record ChatRoomInformationDto(
    Guid Id,
    string Name,
    int ParticipantCount)
{
    internal static ChatRoomInformationDto From(ChatRoom room) =>
        new(Id: room.Id,
            Name: room.Name.Value,
            ParticipantCount: room.Participants.Count);
}