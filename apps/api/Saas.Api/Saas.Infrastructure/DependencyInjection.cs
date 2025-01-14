using Microsoft.Extensions.DependencyInjection;
using Saas.Application.Interfaces;
using Saas.Domain;
using Saas.Infrastructure.Data;

namespace Saas.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}