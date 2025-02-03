using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Saas.Application.Interfaces;
using Saas.Application.UseCases;
using Saas.Application.UseCases.Auth;
using Saas.Application.UseCases.ChatRooms;
using Saas.Application.UseCases.Posts;
using Saas.Application.UseCases.Users;

namespace Saas.Application;

public static class DependencyInjection
{
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
    }
}