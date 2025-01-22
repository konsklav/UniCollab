using Saas.Domain;

namespace Saas.Api.Contracts;

/// <summary>
/// DTO for essential user information.
/// </summary>
/// <param name="Id">User's unique ID</param>
/// <param name="Username">Username, not unique!</param>
public sealed record UserInformationDto(
    Guid Id,
    string Username)
{
    internal static UserInformationDto From(User user) =>
        new(Id: user.Id,
            Username: user.Username);
}