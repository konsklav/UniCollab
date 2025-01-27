using Ardalis.Result;
using Saas.Application.Common.Authentication;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Authentication;
using Saas.Application.Interfaces.Data;

namespace Saas.Application.UseCases.Auth;

/// <summary>
/// Orchestrates the case where the user logs in using 'Basic Authentication'
/// </summary>
public sealed class BasicLogicUseCase(
    IUserRepository userRepository,
    IAuthenticationHelper authenticationHelper) : IApplicationUseCase
{
    public async Task<Result<AuthenticatedUser>> Handle(string username, string password)
    {
        var user = await userRepository.GetByCredentialsAsync(username, password);
        if (user is null)
            return Result.NotFound();

        var token = authenticationHelper.GenerateToken(user);
        return new AuthenticatedUser(user, token);
    }
}