using Microsoft.Extensions.DependencyInjection;
using Saas.Application.UseCases;

namespace Saas.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AddFriendUseCase>();
        services.AddScoped<GetUserUseCase>();
        services.AddScoped<RemoveFriendUseCase>();
    }
}