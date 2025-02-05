namespace Saas.Api.Contracts.Queries;

public sealed record GetUsersQuery(string? Info, Guid? Target)
{
    internal GetUsersQueryType Type => (Info, Target) switch
    {
        ("detail", not null) => GetUsersQueryType.Detailed,
        _ => GetUsersQueryType.Regular
    };
}