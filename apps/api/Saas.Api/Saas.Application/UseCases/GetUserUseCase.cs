using Ardalis.Result;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases;

public class GetUserUseCase(IUserRepository repository)
{
    public async Task<Result<User>> Handle(Guid userId)
    {
        var user = await repository.GetUserByIdAsync(userId);
        if (user is null) 
            return Result<User>.NotFound();

        return user;
    }
}