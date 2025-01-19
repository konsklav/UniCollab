using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Saas.Application.UseCases;

namespace Saas.Api.Endpoints;

[ApiController]
[Route("/users/{userId:guid}/friends")]
public class FriendsController : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetAll(Guid userId, [FromServices] GetUserUseCase getUser)
    {
        var result = await getUser.Handle(userId);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();

        var user = result.Value;
        return Results.Ok(user.Friends);
    }

    [HttpPut("{friendId:guid}")]
    public async Task<IResult> AddFriend(Guid userId, Guid friendId, [FromServices] AddFriendUseCase addFriend)
    {
        var result = await addFriend.Handle(userId, friendId);
        return result.ToMinimalApiResult();
    }

    [HttpDelete("{friendId:guid}")]
    public async Task<IResult> RemoveFriend(Guid userId, Guid friendId, [FromServices] RemoveFriendUseCase removeFriend)
    {
        var result = await removeFriend.Handle(userId, friendId);
        return result.ToMinimalApiResult();
    }
}