using Ardalis.Result;
using Saas.Application.Interfaces.Data;

namespace Saas.Application.UseCases;

public class RemoveFriendUseCase(IUserRepository userRepository)
{
    public async Task<Result> Handle(Guid userId, Guid friendToAddId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        var friendToRemove = await userRepository.GetUserByIdAsync(friendToAddId);

        if (user is null || friendToRemove is null)
            return Result.NotFound();
    
        var removeFriendResult = user.RemoveFriend(friendToRemove);

        if (!removeFriendResult.IsSuccess)
            return removeFriendResult;

        await userRepository.SaveChangesAsync();
        
        return Result.Success();
    }
}