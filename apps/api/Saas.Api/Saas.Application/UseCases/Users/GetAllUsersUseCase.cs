using Ardalis.Result;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases;

public class GetAllUsersUseCase(IUserRepository repository)
{
    public async Task<Result<List<User>>> Handle()
    {
        var users = await repository.GetAllAsync();
        return users;
    }
}