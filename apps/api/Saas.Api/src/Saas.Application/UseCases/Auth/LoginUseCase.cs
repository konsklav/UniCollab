using Ardalis.Result;
using Saas.Application.Authentication;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;

namespace Saas.Application.UseCases.Auth;

/// <summary>
/// Orchestrates the case where the user logs in using 'Basic Authentication'
/// </summary>
public sealed class LoginUseCase(
    IUserRepository userRepository,
    IAuthenticationHelper authenticationHelper,
    CreateUser createUser) : IApplicationUseCase
{
    public async Task<Result<AuthenticatedUser>> BasicAsync(string username, string password)
    {
        var user = await userRepository.GetByBasicCredentialsAsync(username, password);
        if (user is null)
            return Result.NotFound();

        var token = authenticationHelper.GenerateBasicToken(user);
        return new AuthenticatedUser(user, token);
    }

    public async Task<Result<AuthenticatedUser>> GoogleAsync(string googleToken)
    {
        var parseResult = await authenticationHelper.ParseGoogleToken(googleToken);
        if (!parseResult.IsSuccess)
            return parseResult.Map();

        var tokens = parseResult.Value;
        var email = tokens.GooglePayload.Email;
        var googleId = tokens.GooglePayload.Subject;

        if (!await userRepository.GoogleIdExistsAsync(googleId))
        {
            var createResult = await createUser.WithGoogleAsync(email, googleId);
            if (!createResult.IsSuccess)
                return createResult.Map();
        }
        
        var user = await userRepository.GetByGoogleCredentialsAsync(email, googleId);
        if (user is null)
            return Result.NotFound();

        return new AuthenticatedUser(user, tokens.ApplicationToken);
    } 
}