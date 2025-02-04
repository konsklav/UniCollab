using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.Groups;

public class CreateGroup(
    IUserRepository userRepository,
    IGroupRepository groupRepository) : IApplicationUseCase
{
    public async Task<Result<Group>> Handle(string name, List<Guid> userIds, Guid creatorId)
    {
        var users = await userRepository.GetByIdsAsync(userIds);
        if (users.Count != userIds.Count)
            return Result.NotFound("One or more users do not exist.");

        var creator = await userRepository.GetByIdAsync(creatorId);
        if (creator is null)
            return Result.NotFound($"Could not find the creator (user with id: {creatorId}).");
        
        users.Add(creator);
        
        var groupCreationResult = Group.Create(name, users, creator);
        if (!groupCreationResult.IsSuccess)
            return groupCreationResult;

        var group = groupCreationResult.Value;

        groupRepository.Add(group);
        
        await groupRepository.SaveChangesAsync();

        return group;
    }
}