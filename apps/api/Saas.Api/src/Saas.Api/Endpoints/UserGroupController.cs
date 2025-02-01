using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Api.Contracts.Requests;
using Saas.Application.UseCases.Groups;

namespace Saas.Api.Endpoints;

/// <summary>
/// How a user can interact with the group.
/// </summary>
[ApiController]
[Route("/users/{userId:guid}/groups")]
[Authorize]
public class UserGroupController : ControllerBase
{
    [HttpPost(Name = "Create Group")]
    public async Task<IResult> Create(
        [FromRoute] Guid creatorId,
        [FromBody] CreateGroupRequest request, 
        [FromServices] CreateGroup createGroup)
    {
        var result = await createGroup.Handle(
            name: request.Name,
            userIds: request.InitialMembers,
            creatorId: creatorId);

        if (!result.IsSuccess)
            return result.ToMinimalApiResult();

        var group = result.Value;
        return Results.CreatedAtRoute(
            routeName: "Get Group",
            routeValues: new { chatRoomId = group.Id },
            value: GroupDto.From(group));
    }
}