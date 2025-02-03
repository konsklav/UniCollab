using System.Collections.Concurrent;
using Saas.Application.Contracts;
using Saas.Domain;

namespace Saas.Api.Contracts;

public sealed record ChatRoomInformationDto(
    Guid Id,
    string Name,
    MessageDto? LastMessage,
    int ParticipantCount)
{
    internal static ChatRoomInformationDto From(ChatRoom room)
    {
        var lastMessage = room.Messages?.MaxBy(m => m.SentAt);
        
        return new ChatRoomInformationDto(Id: room.Id,
            Name: room.Name.Value,
            ParticipantCount: room.Participants?.Count ?? 0,
            LastMessage: lastMessage != null ? MessageDto.From(lastMessage) : null);
    }
}