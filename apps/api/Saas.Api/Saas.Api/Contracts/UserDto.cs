using Saas.Domain;

namespace Saas.Api.Contracts;

/// <summary>
/// Detailed DTO of a user.
/// </summary>
/// <param name="Id">Unique user ID</param>
/// <param name="Username">Username, not unique!</param>
/// <param name="Friends">The user's current friend list.</param>
/// <param name="PostsSlugs">The user's posts shown by their slugs only.</param>
public sealed record UserDto(
    Guid Id,
    string Username,
    List<UserInformationDto> Friends,
    List<string> PostsSlugs)
{
    internal static UserDto From(User user) =>
        new(Id: user.Id,
            Username: user.Username,
            Friends: user.Friends.Select(UserInformationDto.From).ToList(),
            PostsSlugs: user.Posts.Select(p => p.Slug).ToList());
}