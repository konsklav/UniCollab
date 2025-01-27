using Ardalis.Result;
using Saas.Application.Interfaces.Data;

namespace Saas.Application.UseCases.Users;

public class AddFriendUseCase(IUserRepository userRepository)
{
    public async Task<Result> Handle(Guid userId, Guid friendToAddId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        var friendToAdd = await userRepository.GetByIdAsync(friendToAddId);

        if (user is null || friendToAdd is null)
            return Result.NotFound();
    
        var addFriendResult = user.AddFriend(friendToAdd);

        if (!addFriendResult.IsSuccess)
            return addFriendResult;

        await userRepository.SaveChangesAsync();
        
        return Result.Success();
    }
}