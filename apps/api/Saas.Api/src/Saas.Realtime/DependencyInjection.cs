using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Saas.Application.Interfaces;
using Saas.Realtime.Hubs;

namespace Saas.Realtime;

public static class DependencyInjection
{
    public static void AddRealtimeCapabilities(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddScoped<INotificationService, NotificationService>();
    }
    
    public static void MapHubs(this IEndpointRouteBuilder app)
    {
        app.MapHub<ChatHub>("/hubs/chat").RequireAuthorization();
        app.MapHub<NotificationHub>("/hubs/notifications").RequireAuthorization();
    }
}