using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;

namespace Saas.Application.UseCases.Groups;

public class LeaveGroup(
    IUserRepository userRepository,
    IGroupRepository groupRepository) : IApplicationUseCase
{
    public async Task<Result> HandleAsync(Guid groupId, Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result.NotFound($"User with ID '{userId}' not found.");

        var group = await groupRepository.GetByIdAsync(groupId);
        if (group is null)
            return Result.NotFound($"Group with ID '{groupId}' not found.");

        var joinResult = user.LeaveGroup(group);
        
        await groupRepository.SaveChangesAsync();
        
        return joinResult;
    }
}