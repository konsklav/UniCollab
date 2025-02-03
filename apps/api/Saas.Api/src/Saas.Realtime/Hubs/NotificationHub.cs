using Microsoft.AspNetCore.SignalR;
using Saas.Application.Common.Events;
using Saas.Application.Common.Notifications;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Realtime;
using Saas.Application.UseCases.ChatRooms;

namespace Saas.Realtime.Hubs;

public class NotificationHub : Hub<INotificationClient>
{
    private readonly GetChatRooms _getChatRooms;

    public NotificationHub(IEventService eventService, GetChatRooms getChatRooms)
    {
        _getChatRooms = getChatRooms;
        eventService.Subscribe<ChatMessageSentEvent>(async e =>
        {
            var notification = new Notification(NotificationType.ChatMessage, e.Message.Content);
            
            await Clients
                .GroupExcept(e.ChatId.ToString(), e.SenderConnectionId)
                .GetNotification(notification.ToDto());
        });
    }

    /// <summary>
    /// When a client calls this method, the hub will configure appropriate notifications for the user's client.
    /// </summary>
    public async Task Register(Guid userId)
    {
        var getChatResult = await _getChatRooms.Participating(userId);
        if (!getChatResult.IsSuccess)
            return;

        var participatingChats = getChatResult.Value;
        var joinTasks = participatingChats.Select(chat => 
            Groups.AddToGroupAsync(Context.ConnectionId, chat.Id.ToString()));

        await Task.WhenAll(joinTasks);
    }
}