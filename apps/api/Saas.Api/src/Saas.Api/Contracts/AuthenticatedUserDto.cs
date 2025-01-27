using Saas.Application.Common.Authentication;

namespace Saas.Api.Contracts;

public sealed record AuthenticatedUserDto(UserInformationDto User, string Token)
{
    internal static AuthenticatedUserDto From(AuthenticatedUser user) =>
        new(User: UserInformationDto.From(user.User),
            Token: user.Token.Value);
}