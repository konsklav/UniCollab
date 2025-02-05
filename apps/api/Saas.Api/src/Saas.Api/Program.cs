using System.Reflection;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc.Authorization;
using Saas.Api.Authentication;
using Saas.Api.Configuration;
using Saas.Application;
using Saas.Infrastructure;
using Saas.Realtime;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables("UNICOLLAB_");

builder.Services.AddControllers(op => op.Filters.Add(new AuthorizeFilter()));
builder.Services.ConfigureCors(builder.Configuration);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Basic";
    })
    .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>("Basic", null);

builder.Services.AddSwaggerGen(options =>
{
    var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xml));
});

builder.Services.AddApplication();
builder.Services.AddRealtimeCapabilities();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
var corsOrigin = builder.Configuration.GetSection("Cors:Origin").Get<string>();
logger.LogInformation("Using CORS: {cors}", corsOrigin);

app.UseSwagger();
app.UseConfiguredCors();
app.UseAuthorization();
app.MapControllers();
app.MapHubs();

app.Run();