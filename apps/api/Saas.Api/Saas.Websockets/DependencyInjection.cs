using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Saas.Websockets.Hubs;

namespace Saas.Websockets;

public static class DependencyInjection
{
    public static void AddRealtimeCapabilities(this IServiceCollection services)
    {
        services.AddSignalR();
    }
    
    public static void MapHubs(this IEndpointRouteBuilder app)
    {
        app.MapHub<ChatHub>("/hubs/chat");
        app.MapHub<NotificationHub>("/hubs/notifications");
    }
}