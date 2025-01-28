using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Api.Contracts.Requests;
using Saas.Application.UseCases.ChatRooms;

namespace Saas.Api.Endpoints;

[ApiController]
[Route("/chat")]
[Authorize]
public class ChatRoomController : ControllerBase
{
    /// <summary>
    /// Retrieve a chatroom by their ID, if found.
    /// </summary>
    /// <param name="chatRoomId">The ID to search.</param>
    /// <param name="getChatRoom"></param>
    /// <returns>A chat room</returns>
    [HttpGet("{chatRoomId:guid}", Name = "Get Chat")]
    public async Task<IResult> Get(
        [FromRoute] Guid chatRoomId, 
        [FromServices] GetChatRoomUseCase getChatRoom)
    {
        var result = await getChatRoom.Handle(chatRoomId);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();
    
        var chatRoom = result.Value;
        return Results.Ok(ChatRoomDto.From(chatRoom));
    }

    [HttpPost(Name = "Create Chat")]
    public async Task<IResult> Create(
        [FromBody] CreateChatRoomRequest request, 
        [FromServices] CreateChatRoomUseCase createChatRoom)
    {
        var result = await createChatRoom.Handle(
            name: request.Name,
            userIds: request.InitialParticipants);

        if (!result.IsSuccess)
            return result.ToMinimalApiResult();

        var chatRoom = result.Value;
        return Results.CreatedAtRoute(
            routeName: "Get Chat",
            routeValues: new { chatRoomId = chatRoom.Id },
            value: chatRoom);
    }
    
    /// <summary>
    /// Retrieve all the chat rooms that the user can join.
    /// </summary>
    /// <returns>A list of chatrooms</returns>
    [HttpGet("joinable/{userId:guid}", Name = "Get Joinable Chats")]
    public async Task<IResult> GetJoinable(Guid userId, [FromServices] GetJoinableChatRooms getJoinableChatRooms)
    {
        var result = await getJoinableChatRooms.Handle(userId);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();

        var chatRooms = result.Value;
        return Results.Ok(chatRooms.Select(ChatRoomInformationDto.From));
    }
}