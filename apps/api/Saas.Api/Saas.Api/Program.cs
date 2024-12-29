using Saas.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseConfiguredCors();

app.MapGet("/test", () => $"Testing! The time is {DateTime.UtcNow}");
app.MapGet("/number", () => Random.Shared.Next(100));

app.Run(); 