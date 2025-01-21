using Ardalis.Result;
using Saas.Application.Interfaces.Data;
using Saas.Domain;
using Saas.Domain.Posts;

namespace Saas.Application.UseCases;

public class GetUsersPostsUseCase(IUserRepository repository)
{
    public async Task<Result<List<Post>>> Handle(Guid userId)
    {
        var user  = await repository.GetByIdAsync(userId);
        if (user is null)
            return Result<List<Post>>.NotFound("User not found");
        
        return user.Posts.ToList();
    }
}