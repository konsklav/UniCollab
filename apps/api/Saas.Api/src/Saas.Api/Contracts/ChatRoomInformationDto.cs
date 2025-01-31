using System.Collections.Concurrent;
using Saas.Domain;

namespace Saas.Api.Contracts;

public sealed record ChatRoomInformationDto(
    Guid Id,
    string Name,
    MessageDto? LastMessage,
    int ParticipantCount)
{
    internal static ChatRoomInformationDto From(ChatRoom room) =>
        new(Id: room.Id,
            Name: room.Name.Value,
            ParticipantCount: room.Participants?.Count ?? 0,
            LastMessage: MessageDto.From(room.Messages?.MaxBy(m => m.SentAt)));
}