using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Api.Contracts.Queries;
using Saas.Api.Extensions;
using Saas.Application.UseCases.ChatRooms;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Saas.Api.Endpoints;

/// <summary>
/// How a user can interact with the chat.
/// </summary>
[ApiController]
[Route("/users/{userId:guid}/chats")]
public class UserChatController
{
    /// <summary>
    /// Attempts to insert the specified user in the specified chat room.
    /// </summary>
    [HttpPost("{chatId:guid}", Name = "Join Chat")]
    public async Task<IResult> JoinChat(
        [FromRoute] Guid chatId, 
        [FromRoute] Guid userId,
        [FromServices] JoinChatRoom joinChatRoom)
    {
        return await joinChatRoom.HandleAsync(chatId: chatId, userId: userId).ToHttp();
    }

    /// <summary>
    /// Attempts to remove the specified user from the specified chat room.
    /// </summary>
    [HttpDelete("{chatId:guid}", Name = "Leave Chat")]
    public async Task<IResult> LeaveChat(
        [FromRoute] Guid chatId,
        [FromRoute] Guid userId,
        [FromServices] LeaveChatRoom leaveChatRoom)
    {
        return await leaveChatRoom.HandleAsync(chatId, userId).ToHttp();
    }

    /// <summary>
    /// Gets the chat rooms of a user. You can query parameters to specify what types of chats you want returned.
    /// </summary>
    [HttpGet]
    public async Task<IResult> Get(
        [FromRoute] Guid userId,
        [FromQuery] GetChatRoomQuery query,
        [FromServices] GetChatRooms getChatRooms)
    {
        return query.QueryType switch
        {
            ChatQueryType.Joinable => (await getChatRooms.Joinable(userId)).ToHttp(
                onSuccess: rooms => rooms.Select(ChatRoomInformationDto.From)),
            ChatQueryType.Participating => (await getChatRooms.Participating(userId)).ToHttp(
                onSuccess: rooms => rooms.Select(ChatRoomInformationDto.From)),
            _ => Results.BadRequest("Unknown 'Type' parameter.")
        };
    }
}