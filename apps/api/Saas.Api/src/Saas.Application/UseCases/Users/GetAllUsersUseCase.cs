using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Application.Models;
using Saas.Domain;

namespace Saas.Application.UseCases.Users;

public class GetAllUsersUseCase(
    GetUserUseCase getUser,
    IChatRoomRepository chatRepository,
    IUserRepository userRepository) : IApplicationUseCase
{
    public async Task<Result<List<User>>> Handle()
    {
        var users = await userRepository.GetAllAsync();
        return users;
    }

    
    public async Task<Result<List<DetailedUserInformation>>> HandleDetailedAsync(Guid targetId)
    {
        var targetUserResult = await getUser.Handle(targetId);
        if (!targetUserResult.IsSuccess)
            return targetUserResult.Map();

        var targetUser = targetUserResult.Value;
        var users = await userRepository.GetAllAsync();
        var detailedUsers = new List<DetailedUserInformation>();

        foreach (var user in users.Except([targetUser]))
        {
            var mutualFriends = user.Friends.Intersect(targetUser.Friends);
            var mutualChats = await chatRepository.GetMutualChatsOf(user.Id, targetUser.Id);

            var postsPerSubject = user.Posts
                .SelectMany(p => p.Subjects)
                .GroupBy(s => s)
                .Select(group => new SubjectCount(group.Key, group.Count()));
            
            detailedUsers.Add(new DetailedUserInformation(
                User: user,
                IsFriend: targetUser.Friends.Contains(user),
                MutualFriends: mutualFriends.ToList(),
                MutualChats: mutualChats,
                TotalPostsUploaded: user.Posts.Count,
                PostsPerSubject: postsPerSubject.ToList()));
        }

        return detailedUsers;
    }
}