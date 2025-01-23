using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;
using Saas.Infrastructure.Events;
using Saas.Infrastructure.Repositories;
using Saas.Tests.Fakes;

namespace Saas.Infrastructure;

public static class DependencyInjection
{
    public static async Task AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
        services.AddScoped<IEventService, EventService>();
        
        services.AddDbContext<UniCollabContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
            options.LogTo(Console.WriteLine, LogLevel.Information);
            options.UseSeeding((context, _) =>
            {
                if (!isDevelopment)
                    return;
                
                var user = context.Set<User>().FirstOrDefault();
                if (user is not null)
                    return;

                SeedDatabase(context);
                context.SaveChanges();
            });
            options.UseAsyncSeeding(async (context, _, ct) =>
            {
                if (!isDevelopment)
                    return;
                
                var user = await context.Set<User>().FirstOrDefaultAsync(cancellationToken: ct);
                if (user is not null)
                    return;
                
                SeedDatabase(context);
                await context.SaveChangesAsync(ct);
            });
        });
        
        if (!isDevelopment) 
            return;

        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UniCollabContext>();

        await context.Database.EnsureCreatedAsync();
    }

    private static void SeedDatabase(DbContext context)
    {
        if (context is not UniCollabContext uniContext)
            throw new InvalidOperationException("Expected to seed UniCollabContext.");

        var users = FakeUsers.Generate(5);
        uniContext.Users.AddRange(users);
                
        users.ForEach(u =>
        {
            var posts = FakePosts.GetForUser(u, 3);
            uniContext.Posts.AddRange(posts);
        });
    }
}