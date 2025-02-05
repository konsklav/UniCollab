using System.Text;
using Ardalis.Result.AspNetCore;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Saas.Api.Contracts;
using Saas.Api.Contracts.Requests;
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
    
    /// <summary>
    /// Login using the Google Authentication scheme
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login/google", Name = "Login using Google")]
    public async Task<IResult> Login(
        [FromBody] GoogleAuthRequest request, 
        [FromServices] JwtHelper jwtHelper)
    {
        var jwtInfo = jwtHelper.GetInfo();
        
        if (request is null || string.IsNullOrEmpty(request.Token))
        {
            return Results.BadRequest("Invalid Google token.");
        }

        try
        {
            // Verify Google ID Token
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token);

            // Create an internal JWT for your application
            var claims = new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, payload.Subject),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, payload.Name),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Email, payload.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: jwtInfo.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);

            return Results.Ok(new { jwt });
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Google authentication failed: {ex.Message}");
        }
    }
}