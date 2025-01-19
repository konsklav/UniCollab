using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Saas.Application.UseCases;

namespace Saas.Api.Endpoints;

[ApiController]
[Route("/users")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetAll([FromServices] GetAllUsersUseCase getAllUsers)
    {
        var result = await getAllUsers.Handle();
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();

        var users = result.Value;
        return Results.Ok(users);
    }

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