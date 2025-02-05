using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Api.Contracts.Queries;
using Saas.Api.Extensions;
using Saas.Application.Contracts;
using Saas.Application.UseCases.Users;

namespace Saas.Api.Endpoints.Users;

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
    public async Task<IResult> GetAll(
        [FromServices] GetAllUsersUseCase getAllUsers,
        [FromQuery] GetUsersQuery query)
    {
        return query.Type switch
        {
            GetUsersQueryType.Detailed when query.Target.HasValue => await getAllUsers
                .HandleDetailedAsync(query.Target.Value)
                .ToHttp(userInfo => userInfo.Select(RichUserInformationDto.From)),
                
            _ => await getAllUsers
                .Handle()
                .ToHttp(users => users.Select(UserInformationDto.From))
        };
    }

    /// <summary>
    /// Retrieve a user by their ID, if found.
    /// </summary>
    /// <param name="userId">The ID to search.</param>
    /// <param name="getUser"></param>
    /// <returns>A user</returns>
    [HttpGet("{userId:guid}", Name = "Get User by ID")]
    public async Task<IResult> Get(Guid userId, [FromServices] GetUserUseCase getUser)
    {
        var result = await getUser.Handle(userId);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();
    
        var user = result.Value;
        return Results.Ok(UserDto.From(user));
    }
}