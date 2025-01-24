using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Application.UseCases;
using Saas.Application.UseCases.Users;

namespace Saas.Api.Endpoints;

/// <summary>
/// Actions for retrieving/modifying users.
/// </summary>
[ApiController]
[Route("/users")]
[Authorize]
public class UsersController : ControllerBase
{
    /// <summary>
    /// Retrieve all the users in the system.
    /// </summary>
    /// <returns>A list of users</returns>
    [HttpGet]
    public async Task<IResult> GetAll([FromServices] GetAllUsersUseCase getAllUsers)
    {
        var result = await getAllUsers.Handle();
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();

        var users = result.Value;
        return Results.Ok(users.Select(UserInformationDto.From));
    }

    /// <summary>
    /// Retrieve a user by their ID, if found.
    /// </summary>
    /// <param name="userId">The ID to search.</param>
    /// <param name="getUser"></param>
    /// <returns>A user</returns>
    [HttpGet("{userId:guid}")]
    public async Task<IResult> Get(Guid userId, [FromServices] GetUserUseCase getUser)
    {
        var result = await getUser.Handle(userId);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();
    
        var user = result.Value;
        return Results.Ok(UserDto.From(user));
    }

    /// <summary>
    /// Retrieve all users posts, if found.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="getUsersPosts"></param>
    /// <returns>A list of posts</returns>
    [HttpGet("{userId:guid}/posts")]
    public async Task<IResult> GetUsersPosts(Guid userId, [FromServices] GetUsersPostsUseCase getUsersPosts)
    {
        var result = await getUsersPosts.Handle(userId);
        if(!result.IsSuccess)
            return result.ToMinimalApiResult();
        
        var posts = result.Value;
        return Results.Ok(posts.Select(PostDto.From));
    }
}