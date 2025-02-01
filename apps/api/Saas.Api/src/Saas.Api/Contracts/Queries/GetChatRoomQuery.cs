namespace Saas.Api.Contracts.Queries;

/// <summary>
/// Parameters to query the application's chat rooms
/// </summary>
/// <param name="Type">The type of chats to get.</param>
public sealed record GetChatRoomQuery(string Type)
{
    internal ChatQueryType QueryType => Type.Trim().ToLower() switch
    {
        "joinable" => ChatQueryType.Joinable,
        "participating" => ChatQueryType.Participating,
        _ => ChatQueryType.Unknown
    };
}

internal enum ChatQueryType
{
    Unknown,
    Joinable,
    Participating
}