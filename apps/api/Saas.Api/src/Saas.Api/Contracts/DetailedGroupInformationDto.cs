using Saas.Application.Contracts;
using Saas.Application.Models;

namespace Saas.Api.Contracts;

public sealed record DetailedGroupInformationDto(
    GroupInformationDto Info,
    IEnumerable<UserInformationDto> FriendsInGroup)
{
    internal static DetailedGroupInformationDto From(DetailedGroupInformation info) =>
        new(Info: GroupInformationDto.From(info.Group),
            FriendsInGroup: info.FriendsInGroup.Select(UserInformationDto.From));
}