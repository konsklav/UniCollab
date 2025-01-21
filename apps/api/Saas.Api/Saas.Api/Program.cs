using System.Reflection;
using Saas.Api.Configuration;
using Saas.Application;
using Saas.Infrastructure;
using Saas.Websockets;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables("UNICOLLAB_");

builder.Services.AddControllers();
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.AddSwaggerGen(options =>
{
    var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xml));
});

builder.Services.AddApplication();
builder.Services.AddRealtimeCapabilities();
await builder.Services.AddInfrastructure(builder.Configuration, isDevelopment: builder.Environment.IsDevelopment());

var app = builder.Build();

app.UseSwagger();
app.UseConfiguredCors();

app.MapControllers();
app.MapHubs();
app.Run();