using Saas.Domain;

namespace Saas.Application.Models;

/// <summary>
/// A wrapper class for <see cref="User"/> that 
/// </summary>
public sealed record DetailedUserInformation(
    User User,
    bool IsFriend,
    List<User> MutualFriends,
    List<ChatRoom> MutualChats,
    int TotalPostsUploaded,
    List<SubjectCount> PostsPerSubject);