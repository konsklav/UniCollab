using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Saas.Application.Interfaces;
using Saas.Infrastructure;
using Saas.Realtime.Hubs;

namespace Saas.Realtime;

public static class DependencyInjection
{
    public static void AddRealtimeCapabilities(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddEvents(Assembly.GetAssembly(typeof(NotificationService))!);
    }
    
    public static void MapHubs(this IEndpointRouteBuilder app)
    {
        app.MapHub<ChatHub>("/hubs/chat");
        app.MapHub<NotificationHub>("/hubs/notifications");
    }
}