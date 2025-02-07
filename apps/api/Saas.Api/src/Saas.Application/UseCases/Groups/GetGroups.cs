using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Application.Models;
using Saas.Application.UseCases.Users;
using Saas.Domain;

namespace Saas.Application.UseCases.Groups;

public class GetGroups(
    GetUserUseCase getUser,
    IGroupRepository groupRepository) : IApplicationUseCase
{
    /// <summary>
    /// Gets groups that user specified by <paramref name="userId"/> is a member of.
    /// </summary>
    public async Task<Result<List<DetailedGroupInformation>>> Participating(Guid userId)
        => await GetGroupsCore(userId, groupRepository.GetByUserAsync);

    /// <summary>
    /// Gets groups that user specified by <paramref name="userId"/> can join.
    /// </summary>
    public async Task<Result<List<DetailedGroupInformation>>> Joinable(Guid userId)
        => await GetGroupsCore(userId, groupRepository.GetJoinableFor);

    private async Task<Result<List<DetailedGroupInformation>>> GetGroupsCore(
        Guid userId,
        Func<Guid, Task<List<Group>>> groupQuery)
    {
        var userGetResult = await getUser.Handle(userId);
        if (!userGetResult.IsSuccess)
            return userGetResult.Map();

        var user = userGetResult.Value;
        var groups = await groupQuery(user.Id);
        
        return groups.Select(group => new DetailedGroupInformation(
                Group: group,
                FriendsInGroup: group.Members.Intersect(user.Friends)))
            .ToList();
    }
}