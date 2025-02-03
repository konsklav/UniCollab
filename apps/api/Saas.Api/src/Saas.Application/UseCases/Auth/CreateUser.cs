using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.Auth;

public class CreateUser(IUserRepository userRepository) : IApplicationUseCase
{
    public async Task<Result<User>> HandleAsync(string username, string password)
    {
        var existingUser = await userRepository.GetByUsernameAsync(username);
        if (existingUser != null)
            return Result.Conflict($"User with name '{username}' already exists!");

        var createResult = User.Create(username, password);
        if (!createResult.IsSuccess)
            return createResult.Map();

        var user = createResult.Value;
        userRepository.Add(user);

        await userRepository.SaveChangesAsync();
        return user;
    }
}