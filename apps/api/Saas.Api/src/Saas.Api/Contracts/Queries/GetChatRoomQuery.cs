namespace Saas.Api.Contracts.Queries;

/// <summary>
/// Parameters to query the application's chat rooms
/// </summary>
/// <param name="Type">The type of chats to get.</param>
public sealed record GetChatRoomQuery(string Type)
{
    internal bool IsForJoinableChats() => Type.Equals("joinable", StringComparison.OrdinalIgnoreCase);
    internal bool IsForParticipatingChats() => Type.Equals("participating", StringComparison.OrdinalIgnoreCase);
}
