using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Application.UseCases;

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
        return Results.Ok(users);
    }

    /// <summary>
    /// Retrieve a user by their ID, if found.
    /// </summary>
    /// <param name="userId">The ID to search.</param>
    /// <param name="getUser"></param>
    [HttpGet("{userId:guid}")]
    public async Task<IResult> Get(Guid userId, [FromServices] GetUserUseCase getUser)
    {
        var result = await getUser.Handle(userId);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();
    
        var user = result.Value;
        return Results.Ok(user);
    }
}