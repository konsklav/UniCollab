using Saas.Application.Contracts;
using Saas.Domain;

namespace Saas.Api.Contracts;

public sealed record GroupInformationDto(
    Guid Id,
    string Name,
    int MemberCount,
    UserInformationDto Creator)
{
    internal static GroupInformationDto From(Group group) =>
        new(Id: group.Id,
            Name: group.Name.Value,
            MemberCount: group.Members?.Count ?? 0,
            Creator: UserInformationDto.From(group.Creator));
}