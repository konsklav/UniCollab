using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Api.Contracts.Requests;
using Saas.Application.UseCases.Auth;

namespace Saas.Api.Endpoints;

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
    [HttpPost("/login/basic")]
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
}