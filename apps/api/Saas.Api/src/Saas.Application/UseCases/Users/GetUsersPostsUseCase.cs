using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.Users;

public class GetUsersPostsUseCase(IUserRepository repository) : IApplicationUseCase
{
    public async Task<Result<List<Post>>> Handle(Guid userId)
    {
        var user  = await repository.GetByIdAsync(userId);
        if (user is null)
            return Result<List<Post>>.NotFound("User not found");
        
        return user.Posts.ToList();
    }
}