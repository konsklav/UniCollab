using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Saas.Application.Interfaces;
using Saas.Infrastructure.Repositories;

namespace Saas.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<UniCollabContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"));
            options.LogTo(Console.WriteLine, LogLevel.Information);
        });
    }
}