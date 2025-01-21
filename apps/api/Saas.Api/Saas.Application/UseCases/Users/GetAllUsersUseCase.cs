using System.Diagnostics;
using Ardalis.Result;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases;

public class GetAllUsersUseCase(IUserRepository userRepository)
{
    public async Task<Result<List<User>>> Handle()
    {
        var users = await userRepository.GetAllAsync();
        return users;
    }
}