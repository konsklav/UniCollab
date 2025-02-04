using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.Posts;

public class CreatePost(
    IUserRepository userRepository,
    IPostRepository postRepository) : IApplicationUseCase
{
    public async Task<Result<Post>> Handle(string title, string content, List<string> subjects, Guid authorId)
    {
        var author = await userRepository.GetByIdAsync(authorId);
        if (author is null)
            return Result.NotFound($"Could not find the author (user with id: {authorId}).");
        
        // TODO - Subject Repository: Get by IDs 
        
        var postCreationResult = Post.Create(title,  content, subjects, author);
        if (!postCreationResult.IsSuccess)
            return postCreationResult;

        var post = postCreationResult.Value;

        postRepository.Add(post);
        
        await postRepository.SaveChangesAsync();

        return post;
    }
}