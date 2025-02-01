using Saas.Application.Contracts;
using Saas.Domain;

namespace Saas.Api.Contracts;

public sealed record ChatRoomDto(
    Guid Id,
    string Name,
    List<UserInformationDto> Participants,
    List<MessageDto> Messages)
{
    internal static ChatRoomDto From(ChatRoom chatRoom) =>
        new(Id: chatRoom.Id,
            Name: chatRoom.Name.Value,
            Participants: chatRoom.Participants.Select(UserInformationDto.From).ToList(),
            Messages: chatRoom.Messages.Select(MessageDto.From).ToList());
}