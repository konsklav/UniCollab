using Saas.Domain;

namespace Saas.Application.Contracts;

public sealed record RichUserInformationDto(
    UserInformationDto user, 
    bool IsFriend,
    List<UserInformationDto> MutualFriends,
    List<ChatR) 
{
    public static RichUserInformationDto From(User user, User queryUser) =>
        new(Id: user.Id,
            Username: user.Username,
            IsFriend: queryUser.Friends.Contains(user)); 
}