var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.MapGet("/test", () => $"Testing! The time is {DateTime.UtcNow}");

app.Run(); //Test