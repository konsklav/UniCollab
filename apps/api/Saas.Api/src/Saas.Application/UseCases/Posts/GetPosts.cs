using Ardalis.Result;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.Posts;

public sealed class GetPosts(IPostRepository repository)
{
    public async Task<Result<Post>> BySlug(string slug)
    {
        var post = await repository.GetBySlugAsync(slug);
        if (post is null)
            return Result.NotFound($"Couldn't find post with slug '{slug}'");

        return post;
    }

    public async Task<Result<List<Post>>> ByMostRecent(int count)
    {
        var posts = await repository.GetMostRecentAsync(count);
        return posts;
    }

    public async Task<Result<List<Post>>> ByUser(Guid userId)
    {
        var posts = await repository.GetByUserAsync(userId);
        return posts;
    }
} 