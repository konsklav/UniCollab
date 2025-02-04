using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Api.Contracts.Requests;
using Saas.Application.UseCases.Posts;

namespace Saas.Api.Endpoints;

[ApiController]
[Route("/users/{userId:guid}/posts")]
public class UserPostsController : ControllerBase
{
    /// <summary>
    /// Get all posts by a specific user.
    /// </summary>
    /// <param name="userId">The user's ID which will be used to search for posts by them.</param>
    /// <param name="getPosts"></param>
    [HttpGet(Name = "Get User's Posts")]
    public async Task<IResult> GetPostsByUser(
        [FromRoute] Guid userId,
        [FromServices] GetPosts getPosts)
    {
        var result = await getPosts.ByUser(userId);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();

        var posts = result.Value;
        return Results.Ok(posts.Select(PostDto.From));
    }
    
    [HttpPost(Name = "Create Post")]
    public async Task<IResult> CreatePost(
        [FromRoute] Guid userId,
        [FromBody] CreatePostRequest request,
        [FromServices] CreatePost createPost)
    {
        var result = await createPost.Handle(
            title: request.Title,
            content: request.Content,
            subjects: request.Subjects,
            authorId: userId);

        if (!result.IsSuccess)
            return result.ToMinimalApiResult();

        var post = result.Value;
        return Results.Ok(PostDto.From(post));
    }
}