using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Application.Contracts;
using Saas.Application.UseCases.Users;

namespace Saas.Api.Endpoints.Users;

/// <summary>
/// Actions for retrieving/modifying a specific users friends.
/// </summary>
[ApiController]
[Route("/users/{userId:guid}/friends")]
public class FriendsController : ControllerBase
{
    /// <summary>
    /// Retrieve all the friends of the user if the user exists.
    /// </summary>
    /// <param name="userId">The ID of the user to search.</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IResult> GetAll(Guid userId, [FromServices] GetUserUseCase getUser)
    {
        var result = await getUser.Handle(userId);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();

        var user = result.Value;
        return Results.Ok(user.Friends.Select(UserInformationDto.From));
    }

    /// <summary>
    /// Adds the user specified with <paramref name="friendId"/> to the friend list of the user specified with
    /// <paramref name="userId"/>
    /// </summary>
    /// <param name="userId">The user who will get the friend</param>
    /// <param name="friendId">The target</param>
    /// <returns></returns>
    [HttpPost("{friendId:guid}")]
    public async Task<IResult> AddFriend(Guid userId, Guid friendId, [FromServices] AddFriendUseCase addFriend)
    {
        var result = await addFriend.Handle(userId, friendId);
        return result.ToMinimalApiResult();
    }

    /// <summary>
    /// Removes the user specified with <paramref name="friendId"/> from the friend list of the user specified with
    /// <paramref name="userId"/>
    /// </summary>
    /// <param name="userId">The user who will lose a friend</param>
    /// <param name="friendId">The target</param>
    /// <returns></returns>
    [HttpDelete("{friendId:guid}")]
    public async Task<IResult> RemoveFriend(Guid userId, Guid friendId, [FromServices] RemoveFriendUseCase removeFriend)
    {
        var result = await removeFriend.Handle(userId, friendId);
        return result.ToMinimalApiResult();
    }
}