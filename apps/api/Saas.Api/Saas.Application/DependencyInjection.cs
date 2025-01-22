using Microsoft.Extensions.DependencyInjection;
using Saas.Application.UseCases;
using Saas.Application.UseCases.Posts;

namespace Saas.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetChatRoomUseCase>();
        services.AddScoped<GetAllChatRoomsUseCase>();
        services.AddScoped<AddFriendUseCase>();
        services.AddScoped<RemoveFriendUseCase>();
        services.AddScoped<GetUserUseCase>();
        services.AddScoped<GetAllUsersUseCase>();
        services.AddScoped<GetUsersPostsUseCase>();
        services.AddScoped<GetPosts>();
    }
}