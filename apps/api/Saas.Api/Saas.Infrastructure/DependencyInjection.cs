using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;
using Saas.Infrastructure.Events;
using Saas.Infrastructure.Repositories;
using Saas.Infrastructure.Seeding;
using Saas.Tests.Fakes;

namespace Saas.Infrastructure;

public static class DependencyInjection
{
    public static async Task AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        services.AddScoped<IEventService, EventService>();

        services.AddTransient<ISeeder, Seeder>();
        
        services.AddDbContext<UniCollabContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
            options.LogTo(Console.WriteLine, LogLevel.Information);
        });
    }
}