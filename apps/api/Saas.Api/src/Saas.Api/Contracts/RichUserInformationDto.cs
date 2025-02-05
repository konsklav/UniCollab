using Saas.Application.Contracts;
using Saas.Application.Models;
using Saas.Domain;

namespace Saas.Api.Contracts;

public sealed record RichUserInformationDto(
    UserInformationDto User, 
    bool IsFriend,
    IEnumerable<UserInformationDto> MutualFriends,
    IEnumerable<ChatRoomInformationDto> MutualChats,
    int TotalPostsUploaded,
    IEnumerable<SubjectCountDto> PostsPerSubject)
{
    public static RichUserInformationDto From(DetailedUserInformation info) =>
        new(User: UserInformationDto.From(info.User),
            IsFriend: info.IsFriend,
            MutualFriends: info.MutualFriends.Select(UserInformationDto.From),
            MutualChats: info.MutualChats.Select(ChatRoomInformationDto.From),
            TotalPostsUploaded: info.TotalPostsUploaded,
            PostsPerSubject: info.PostsPerSubject.Select(SubjectCountDto.From));
}