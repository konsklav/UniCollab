using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Application.UseCases.Groups;

namespace Saas.Api.Endpoints;

[ApiController]
[Route("/groups")]
[Authorize]
public class GroupController : ControllerBase
{
    /// <summary>
    /// Retrieve a group by their ID, if found.
    /// </summary>
    /// <param name="groupId">The ID to search.</param>
    /// <param name="getGroup"></param>
    /// <returns>A group</returns>
    [HttpGet("{groupId:guid}", Name = "Get Group")]
    public async Task<IResult> Get(
        [FromRoute] Guid groupId, 
        [FromServices] GetGroup getGroup)
    {
        var result = await getGroup.Handle(groupId);
        if (!result.IsSuccess)
            return result.ToMinimalApiResult();
    
        var group = result.Value;
        return Results.Ok(GroupDto.From(group));
    }
}