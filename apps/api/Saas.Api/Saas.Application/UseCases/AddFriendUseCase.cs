using Ardalis.Result;
using Saas.Application.Interfaces.Data;

namespace Saas.Application.UseCases;

public class AddFriendUseCase(IUserRepository userRepository)
{
    public async Task<Result> Handle(Guid userId, Guid friendToAddId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        var friendToAdd = await userRepository.GetUserByIdAsync(friendToAddId);

        if (user is null || friendToAdd is null)
            return Result.NotFound();
    
        var addFriendResult = user.AddFriend(friendToAdd);

        if (!addFriendResult.IsSuccess)
            return addFriendResult;

        await userRepository.SaveChangesAsync();
        
        return Result.Success();
    }
}