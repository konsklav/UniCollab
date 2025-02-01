using Saas.Domain;

namespace Saas.Application.Contracts;

public sealed record MessageDto(
    Guid Id,
    string Content,
    DateTime SentAt,
    UserInformationDto Sender)
{
    public static MessageDto From(Message message) =>
        new(Id: message.Id, 
            Content: message.Content,
            SentAt: message.SentAt, 
            Sender: UserInformationDto.From(message.Sender));
}