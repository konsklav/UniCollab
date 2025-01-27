using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Application.UseCases.Posts;

namespace Saas.Api.Endpoints;

/// <summary>
/// Methods for interacting with/modifying posts
/// </summary>
[ApiController]
[Route("/posts")]
[Authorize]
public class PostsController : ControllerBase
{
    /// <summary>
    /// Retrieve a post by its slug. Slugs are unique so it is guaranteed that you will get back a single post.  
    /// </summary>
    /// <param name="slug">The slug to search for.</param>
    /// <param name="getPost"></param>
    [HttpGet("{slug:required}")]
    public async Task<IResult> GetPostBySlug(string slug, [FromServices] GetPosts getPost)
    {
        var result = await getPost.BySlug(slug);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();

        var post = result.Value;
        return Results.Ok(PostDto.From(post));
    }

    /// <summary>
    /// Get the most recently uploaded posts, the amount is declared in the <paramref name="count"/> parameter.
    /// </summary>
    /// <param name="count">How many posts to retrieve.</param>
    /// <param name="getPosts"></param>
    [HttpGet("recent/{count:int}")]
    public async Task<IResult> GetMostRecentPosts(int count, [FromServices] GetPosts getPosts)
    {
        var result = await getPosts.ByMostRecent(count);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();
        
        var posts = result.Value;
        var postDtos = posts.Select(PostDto.From);
        return Results.Ok(postDtos);
    }
}