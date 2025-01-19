using Saas.Api.Configuration;
using Saas.Application;
using Saas.Infrastructure;
using Saas.Websockets;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddRealtimeCapabilities();
await builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.MapOpenApi();
app.UseConfiguredCors();

app.MapControllers();
app.MapHubs();
app.Run();