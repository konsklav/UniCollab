using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Configuration;
using Saas.Application;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces;
using Saas.Application.UseCases;
using Saas.Infrastructure;
using Saas.Websockets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors(builder.Configuration);
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddRealtimeCapabilities();

var app = builder.Build();

app.MapOpenApi();
app.UseConfiguredCors();

app.MapGet("/test", () => $"Testing! The time is {DateTime.UtcNow}");
app.MapGet("/number", () => Random.Shared.Next(100));

app.MapGet("/users/{userId}/friends/add/{friendId}", AddFriend);
app.MapGet("/users/{userId:guid}/friends", async (Guid userId, [FromServices] GetUserUseCase getUser) =>
{
    var result = await getUser.Handle(userId);
    if (!result.IsSuccess)
        return result.ToMinimalApiResult();

    var user = result.Value;
    return Results.Ok(user.Friends);
});

app.MapGet("/users/{userId}", async (Guid userId, [FromServices] GetUserUseCase getUser) =>
{
    var result = await getUser.Handle(userId);
    if (!result.IsSuccess)
        return result.ToMinimalApiResult();
    
    var user = result.Value;
    return Results.Ok(user);
});

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