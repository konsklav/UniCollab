using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.Result.AspNetCore;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Saas.Api.Contracts;
using Saas.Api.Contracts.Requests;
using Saas.Api.Extensions;
using Saas.Application.Authentication;
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
        [FromServices] LoginUseCase login)
    {
        var loginResult = await login.BasicAsync(request.Username, request.Password);
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
        var createUserResult = await createUser.WithBasicAsync(request.Username, request.Password);
        if (!createUserResult.IsSuccess)
            return createUserResult.ToMinimalApiResult();

        var user = createUserResult.Value;
        return Results.CreatedAtRoute(
            routeName: "Get User by ID",
            routeValues: new { userId = user.Id },
            value: user);
    }
    
    /// <summary>
    /// Login using the Google Authentication scheme
    /// </summary>
    /// <returns></returns>
    [HttpPost("login/google", Name = "Login using Google")]
    public async Task<IResult> Login(
        [FromBody] GoogleAuthRequest request,
        [FromServices] LoginUseCase login)
    {
        return await login
            .GoogleAsync(request.Credential)
            .ToHttp(authenticatedUser => authenticatedUser);
    }
}