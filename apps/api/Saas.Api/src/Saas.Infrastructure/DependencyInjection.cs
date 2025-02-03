using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Saas.Application.Authentication;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Infrastructure.Authentication;
using Saas.Infrastructure.Common;
using Saas.Infrastructure.Data.Repositories;
using Saas.Infrastructure.Data.Seeding;
using Saas.Infrastructure.Events;
using Saas.Infrastructure.Workers;

namespace Saas.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        
        services.AddScoped<IAuthenticationHelper, BasicAuthenticationHelper>();
        
        services.AddTransient<ISeeder, Seeder>();
        services.AddDbContext<UniCollabContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
        });
        
        AddEvents(services, Assembly.GetExecutingAssembly());
    }

    public static void AddEvents(this IServiceCollection services, Assembly assembly)
    {
        // Add a hosted service per calling assembly.
        services.AddHostedService<EventListenerInitializer>(sp => 
            new EventListenerInitializer(assembly, sp));
        
        services.TryAddSingleton<IEventService, EventService>();
        
        ReflectionHelpers.ForEachImplementationOf<IEventListener>(
            assembly: assembly, 
            action: services.TryAddSingleton);
    }
}