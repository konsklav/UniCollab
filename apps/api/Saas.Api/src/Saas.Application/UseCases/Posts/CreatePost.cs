using Ardalis.Result;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.Posts;

public class CreatePost(
    IUserRepository userRepository,
    IPostRepository postRepository,
    ISubjectRepository subjectRepository,
    INotificationService notificationService) : IApplicationUseCase
{
    public async Task<Result<Post>> Handle(string title, string content, List<Guid> subjectIds, Guid authorId)
    {
        var author = await userRepository.GetByIdAsync(authorId);
        if (author is null)
            return Result.NotFound($"Could not find the author (user with ID: {authorId}).");
        
        var subjects = await subjectRepository.GetByIdsAsync(subjectIds);
        if (subjects.Count != subjectIds.Count)
            return Result.NotFound("Couldn't find one or more subjects.");

        var postCreationResult = Post.Create(title,  content, subjects, author);
        if (!postCreationResult.IsSuccess)
            return postCreationResult;

        var post = postCreationResult.Value;

        postRepository.Add(post);
        
        await postRepository.SaveChangesAsync();

        _ = notificationService.SendAsync(new Notification(
            Type: NotificationType.PostUploaded,
            Header: "New Post!",
            Message: $"{author.Username} uploaded a new post.",
            SenderId: author.Id));
        
        return post;
    }
}