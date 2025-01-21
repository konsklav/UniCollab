using Ardalis.Result;
using Saas.Application.Interfaces.Data;
using Saas.Domain.Posts;

namespace Saas.Application.UseCases.Posts;

public sealed class GetPost(IPostRepository repository)
{
    public async Task<Result<Post>> BySlug(string slug)
    {
        var post = await repository.GetBySlugAsync(slug);
        if (post is null)
            return Result.NotFound($"Couldn't find post with slug '{slug}'");

        return post;
    }
} 