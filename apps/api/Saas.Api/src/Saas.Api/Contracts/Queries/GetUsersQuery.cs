namespace Saas.Api.Contracts.Queries;

public sealed record GetUsersQuery(string? Metadata, Guid? Target)
{
    internal GetUsersQueryType Type => (Metadata, Target) switch
    {
        ("friend", not null) => GetUsersQueryType.WithFriendMetadata,
        _ => GetUsersQueryType.Regular
    };
}