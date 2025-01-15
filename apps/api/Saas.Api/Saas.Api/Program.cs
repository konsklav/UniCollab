using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Saas.Api.Configuration;
using Saas.Application;
using Saas.Application.UseCases;
using Saas.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors(builder.Configuration);
builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.MapOpenApi();
app.UseConfiguredCors();

app.MapGet("/test", () => $"Testing! The time is {DateTime.UtcNow}");
app.MapGet("/number", () => Random.Shared.Next(100));

app.MapGet("/user/{userId}/friends/add/{friendId}", AddFriend);
app.MapGet("/user/{userId:guid}/friends", async (Guid userId, [FromServices] GetUserUseCase getUser) =>
{
    var result = await getUser.Handle(userId);
    if (!result.IsSuccess)
        return result.ToMinimalApiResult();

    var user = result.Value;
    return Results.Ok(user.Friends);
});

app.Run();

static async Task<IResult> AddFriend(
    Guid userId, 
    Guid friendId,
    [FromServices] AddFriendUseCase useCase)
{ 
    var result = await useCase.Handle(userId, friendId);
    return result.ToMinimalApiResult();
}