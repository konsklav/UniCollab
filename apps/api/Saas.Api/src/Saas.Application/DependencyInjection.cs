using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped<BasicLogicUseCase>();
        services.AddScoped<GetChatRoomUseCase>();
        services.AddScoped<GetJoinableChatRooms>();
        services.AddScoped<AddFriendUseCase>();
        services.AddScoped<RemoveFriendUseCase>();
        services.AddScoped<GetUserUseCase>();
        services.AddScoped<GetAllUsersUseCase>();
        services.AddScoped<GetUsersPostsUseCase>();
        services.AddScoped<GetPosts>();
    }
}