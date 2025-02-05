using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.Auth;

public class CreateUser(IUserRepository userRepository) : IApplicationUseCase
{
    public async Task<Result<User>> WithBasicAsync(string username, string password) =>
        await CreateUserCore(username, () => User.Create(username, password));

    public async Task<Result<User>> WithGoogleAsync(string username, string googleId) =>
        await CreateUserCore(username, () => User.CreateWithGoogle(username, googleId));

    private async Task<Result<User>> CreateUserCore(string username, Func<Result<User>> userFactory)
    {
        var existingUser = await userRepository.GetByUsernameAsync(username);
        if (existingUser != null)
            return Result.Conflict($"User with name '{username}' already exists!");

        var createResult = userFactory();
        if (!createResult.IsSuccess)
            return createResult.Map();
        
        var user = createResult.Value;
        userRepository.Add(user);

        await userRepository.SaveChangesAsync();
        return user;
    }
}