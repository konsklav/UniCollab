using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Api.Contracts.Requests;
using Saas.Application.UseCases.Auth;

namespace Saas.Api.Endpoints.Users;

/// <summary>
/// Endpoints that allow the end-user to authenticate with this API. 
/// </summary>
[ApiController]
[Route("/auth")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    /// <summary>
    /// Login using the 'Basic Authentication' scheme.
    /// </summary>
    /// <param name="request">The login request model that will be used to authenticate you.</param>
    /// <param name="login"></param>
    /// <returns></returns>
    [HttpPost("login/basic", Name = "Login using Basic Authentication")]
    public async Task<IResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] BasicLogicUseCase login)
    {
        var loginResult = await login.Handle(request.Username, request.Password);
        if (!loginResult.IsSuccess)
            return Results.Unauthorized();

        var authUser = loginResult.Value;
        return Results.Ok(AuthenticatedUserDto.From(authUser));
    }

    /// <summary>
    /// Register a new user with a username and password. If successful, you can then login using Basic Authentication.
    /// </summary>
    [HttpPost("register", Name = "Register a New User")]
    public async Task<IResult> Register(
        [FromBody] RegisterRequest request,
        [FromServices] CreateUser createUser)
    {
        var createUserResult = await createUser.HandleAsync(request.Username, request.Password);
        if (!createUserResult.IsSuccess)
            return createUserResult.ToMinimalApiResult();

        var user = createUserResult.Value;
        return Results.CreatedAtRoute(
            routeName: "Get User by ID",
            routeValues: new { userId = user.Id },
            value: user);
    }
}