using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Application.UseCases.Posts;

namespace Saas.Api.Endpoints;

/// <summary>
/// Methods for interacting with/modifying posts
/// </summary>
[ApiController]
[Route("/posts")]
public class PostsController : ControllerBase
{
    /// <summary>
    /// Retrieve a post by its slug. Slugs are unique so it is guaranteed that you will get back a single post.  
    /// </summary>
    /// <param name="slug">The slug to search for.</param>
    /// <param name="getPosts"></param>
    [HttpGet("{slug:required}")]
    public async Task<IResult> GetPostBySlug(string slug, [FromServices] GetPosts getPost)
    {
        var result = await getPost.BySlug(slug);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();

        var post = result.Value;
        return Results.Ok(PostDto.From(post));
    }

    [HttpGet("{count:int}")]
    public async Task<IResult> GetMostRecentPosts(int count, [FromServices] GetPosts getPosts)
    {
        var result = await getPosts.ByMostRecent(count);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();
        
        var posts = result.Value;
        var postDtos = posts.Select(post => PostDto.From(post));
        return Results.Ok(postDtos);
    }
}