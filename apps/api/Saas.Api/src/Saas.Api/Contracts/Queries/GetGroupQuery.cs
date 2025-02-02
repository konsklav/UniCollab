namespace Saas.Api.Contracts.Queries;

/// <summary>
/// Parameters to query the application's groups
/// </summary>
/// <param name="Type">The type of groups to get.</param>
public sealed record GetGroupQuery(string Type)
{
    internal GroupQueryType QueryType => Type.Trim().ToLower() switch
    {
        "joinable" => GroupQueryType.Joinable,
        "participating" => GroupQueryType.Participating,
        _ => GroupQueryType.Unknown
    };
}

internal enum GroupQueryType
{
    Unknown,
    Joinable,
    Participating
}