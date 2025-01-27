using System.Reflection;
using Saas.Api.Authentication;
using Saas.Api.Configuration;
using Saas.Application;
using Saas.Infrastructure;
using Saas.Realtime;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables("UNICOLLAB_");

builder.Services.AddControllers();
builder.Services.ConfigureCors(builder.Configuration);

builder.Services.AddAuthentication("Basic")
    .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>("Basic", null);

builder.Services.AddSwaggerGen(options =>
{
    var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xml));
});

builder.Services.AddApplication();
builder.Services.AddRealtimeCapabilities();
await builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseConfiguredCors();
app.UseAuthorization();
app.MapControllers();
app.MapHubs();

app.Run();