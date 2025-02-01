using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Api.Contracts.Requests;
using Saas.Api.Extensions;
using Saas.Application.Contracts;
using Saas.Application.UseCases.ChatRooms;

namespace Saas.Api.Endpoints;

[ApiController]
[Route("/chats")]
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
        [FromServices] GetChatRoom getChatRoom)
    {
        var result = await getChatRoom.Handle(chatRoomId);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();
    
        var chatRoom = result.Value;
        return Results.Ok(ChatRoomDto.From(chatRoom));
    }

    /// <summary>
    /// Retrieve all the messages of a chat room.
    /// </summary>
    [HttpGet("{chatId:guid}/messages", Name = "Get Messages of Chat")]
    public async Task<IResult> GetMessages(
        [FromRoute] Guid chatId,
        [FromServices] GetChatMessages getMessages)
    {
        return await getMessages.HandleAsync(chatId).ToHttp(
            onSuccess: messages => messages.Select(MessageDto.From));
    }

    [HttpPost(Name = "Create Chat")]
    public async Task<IResult> Create(
        [FromBody] CreateChatRoomRequest request, 
        [FromServices] CreateChatRoom createChatRoom)
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
            value: ChatRoomDto.From(chatRoom));
    }
}