using Microsoft.AspNetCore.Mvc;

namespace Saas.Api.Endpoints;

/// <summary>
/// Methods for interacting with/modifying posts
/// </summary>
[ApiController]
[Route("/posts")]
public class PostsController
{
    /// <summary>
    /// Retrieve a post by its slug. Slugs are unique so it is guaranteed that you will get back a single post.  
    /// </summary>
    /// <param name="slug">The slug to search for.</param>
    [HttpGet("{slug:alpha}")]
    public async Task<IResult> GetPostBySlug(string slug)
    {
        throw new NotImplementedException();
    }
}