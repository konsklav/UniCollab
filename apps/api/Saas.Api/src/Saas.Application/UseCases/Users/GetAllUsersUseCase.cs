using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.Users;

public class GetAllUsersUseCase(IUserRepository repository) : IApplicationUseCase
{
    public async Task<Result<List<User>>> Handle()
    {
        var users = await repository.GetAllAsync();
        return users;
    }
}