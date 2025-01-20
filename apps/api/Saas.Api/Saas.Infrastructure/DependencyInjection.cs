using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Infrastructure.Events;
using Saas.Infrastructure.Repositories;
using Saas.Tests.Fakes;

namespace Saas.Infrastructure;

public static class DependencyInjection
{
    public static async Task AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEventService, EventService>();
        
        services.AddDbContext<UniCollabContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
            options.LogTo(Console.WriteLine, LogLevel.Information);
        });

        // await MigrateAndAddFakeData(services);
    }

    // private static async Task MigrateAndAddFakeData(IServiceCollection services)
    // {
    //     // Automatic migration when launching.
    //     using var scope = services.BuildServiceProvider().CreateScope();
    //     var dbContext = scope.ServiceProvider.GetRequiredService<UniCollabContext>();
    //
    //     var connected = false;
    //     while (!connected)
    //     {
    //         try
    //         {
    //             await dbContext.Database.MigrateAsync();
    //             connected = true;
    //         }
    //         catch (SqlException)
    //         {
    //             await Task.Delay(400);
    //         }
    //     }
    //     
    //     dbContext.Users.AddRange(FakeUsers.Generate(5));
    //     await dbContext.SaveChangesAsync();
    // }
}