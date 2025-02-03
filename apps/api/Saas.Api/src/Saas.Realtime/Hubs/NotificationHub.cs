using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Saas.Application.Common.Events;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces.Realtime;
using Saas.Application.UseCases.ChatRooms;

namespace Saas.Realtime.Hubs;

public class NotificationHub(GetChatRooms getChatRooms, ILogger<NotificationHub> logger) : Hub<INotificationClient>
{
    private static readonly Dictionary<Guid, string> _userConnections = [];
    internal static IReadOnlyDictionary<Guid, string> UserConnections => _userConnections;
    
    /// <summary>
    /// When a client calls this method, the hub will configure appropriate notifications for the user's client.
    /// </summary>
    public async Task Register(Guid userId)
    {
        if (!_userConnections.TryAdd(userId, Context.ConnectionId))
        {
            logger.LogInformation("Conflict while adding {userId} to notification connections. " +
                                  "Replacing connection ID.", userId);
            
            _userConnections[userId] = Context.ConnectionId;
        }
        
        var getChatResult = await getChatRooms.Participating(userId);
        if (!getChatResult.IsSuccess)
            return;

        var participatingChats = getChatResult.Value;
        var joinTasks = participatingChats.Select(chat => 
            Groups.AddToGroupAsync(Context.ConnectionId, chat.Id.ToString()));

        await Task.WhenAll(joinTasks);
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = _userConnections.FirstOrDefault(kvp => kvp.Value == Context.ConnectionId).Key;
        if (userId != Guid.Empty)
            _userConnections.Remove(userId);

        return Task.CompletedTask;
    }
}