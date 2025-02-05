using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Saas.Application.Authentication;
using Saas.Application.Interfaces;

namespace Saas.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Register application layer services. This includes use cases.
    /// </summary>
    public static void AddApplication(this IServiceCollection services)
    {
        var useCaseInterface = typeof(IApplicationUseCase);
        var useCases = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } &&
                        useCaseInterface.IsAssignableFrom(t));

        foreach (var useCase in useCases)
        {
            services.AddScoped(useCase);
        }

        services.AddScoped<JwtHelper>();
    }
}