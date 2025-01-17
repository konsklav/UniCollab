using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Configuration;
using Saas.Application;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces;
using Saas.Application.UseCases;
using Saas.Infrastructure;
using Saas.Websockets;
using IResult = Microsoft.AspNetCore.Http.IResult;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors(builder.Configuration);
builder.Services.AddOpenApi();

await builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddRealtimeCapabilities();

var app = builder.Build();

app.MapOpenApi();
app.UseConfiguredCors();

app.MapPut("/users/{userId:guid}/friends/{friendId:guid}", AddFriend);
app.MapGet("/users/{userId:guid}/friends", async (Guid userId, [FromServices] GetUserUseCase getUser) =>
{
    var result = await getUser.Handle(userId);
    if (!result.IsSuccess)
        return result.ToMinimalApiResult();

    var user = result.Value;
    return Results.Ok(user.Friends);
});

app.MapGet("/users", async ([FromServices] GetAllUsersUseCase getAllUsers) =>
{
    var result = await getAllUsers.Handle();
    if (!result.IsSuccess)
        return result.ToMinimalApiResult();

    var users = result.Value;
    return Results.Ok(users);
});
app.MapGet("/users/{userId:guid}", async (Guid userId, [FromServices] GetUserUseCase getUser) =>
{
    var result = await getUser.Handle(userId);
    if (!result.IsSuccess)
        return result.ToMinimalApiResult();
    
    var user = result.Value;
    return Results.Ok(user);
});

app.MapDelete("/users/{userId:guid}/friends/{friendId:guid}", RemoveFriend);

app.MapHubs();
app.Run();

static async Task<IResult> AddFriend(
    Guid userId,
    Guid friendId,
    [FromServices] AddFriendUseCase useCase)
{ 
    var result = await useCase.Handle(userId, friendId);
    return result.ToMinimalApiResult();
}

static async Task<IResult> RemoveFriend(
    Guid userId,
    Guid friendId,
    [FromServices] RemoveFriendUseCase useCase)
{
    var result = await useCase.Handle(userId, friendId);
    return result.ToMinimalApiResult();
}