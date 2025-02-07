using Saas.Domain;

namespace Saas.Application.Models;

public sealed record DetailedGroupInformation(
    Group Group,
    IEnumerable<User> FriendsInGroup);
