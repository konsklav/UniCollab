using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;

namespace Saas.Application.UseCases.Users;

public class RemoveFriendUseCase(IUserRepository userRepository) : IApplicationUseCase
{
    public async Task<Result> Handle(Guid userId, Guid friendToAddId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        var friendToRemove = await userRepository.GetByIdAsync(friendToAddId);

        if (user is null || friendToRemove is null)
            return Result.NotFound();
    
        var removeFriendResult = user.RemoveFriend(friendToRemove);

        if (!removeFriendResult.IsSuccess)
            return removeFriendResult;

        await userRepository.SaveChangesAsync();
        
        return Result.Success();
    }
}