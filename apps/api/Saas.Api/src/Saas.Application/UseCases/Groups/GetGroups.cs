using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.Groups;

public class GetGroups(
    IGroupRepository groupRepository) : IApplicationUseCase
{
    /// <summary>
    /// Gets groups that user specified by <paramref name="userId"/> is a member of.
    /// </summary>
    public async Task<Result<List<Group>>> Participating(Guid userId)
    {
        var usersGroups = await groupRepository.GetByUserAsync(userId);
        if (usersGroups.Count == 0)
            return Result.NotFound("Could not find any groups that the user is a member of.");
        return usersGroups;
    }
    
    /// <summary>
    /// Gets groups that user specified by <paramref name="userId"/> can join.
    /// </summary>
    public async Task<Result<List<Group>>> Joinable(Guid userId)
    {
        var chatRooms = await groupRepository.GetJoinableFor(userId);
        return chatRooms;
    }
}