using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Saas.Application.Common.Events;
using Saas.Application.Contracts;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Realtime;
using Saas.Application.UseCases.ChatRooms;
using Saas.Realtime.Contracts;

namespace Saas.Realtime.Hubs;

public record ChatClientInformation(Guid UserId, string ConnectionId);

public class ChatHub(IEventService eventService, AddChatMessageToRoom addMessage) : Hub<IChatClient>
{
    private static readonly Dictionary<Guid, List<ChatClientInformation>> _userConnections = [];
    internal static IReadOnlyDictionary<Guid, List<ChatClientInformation>> UserConnections => _userConnections;
    
    public async Task JoinChat(Guid chatId, Guid userId)
    {
        var userInfo = new ChatClientInformation(userId, Context.ConnectionId);
        if (!_userConnections.TryGetValue(chatId, out var users))
        {
            _userConnections[chatId] = [userInfo];
        }
        else
        {
            users.Add(userInfo);
        }
        
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
    }
    
    public async Task LeaveChat(Guid chatId)
    {
        CleanConnection();
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
    }
    
    public async Task SendMessage(ServerMessage serverMessage)
    {
        var chatGuid = Guid.Parse(serverMessage.ChatId);
        var userGuid = Guid.Parse(serverMessage.UserId);

        var addResult = await addMessage.Handle(
            chatRoomId: chatGuid,
            senderId: userGuid,
            content: serverMessage.Content);

        if (!addResult.IsSuccess)
            return;

        var message = addResult.Value;
        
        var @event = new ChatMessageSentEvent(
            Message: message,
            SenderId: message.Sender.Id,
            ChatId: chatGuid);

        await Clients.Group(chatGuid.ToString()).ReceiveMessage(MessageDto.From(message));
        await eventService.PublishAsync(@event);
    }
    
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        CleanConnection();
        return Task.CompletedTask;
    }
    
    private void CleanConnection()
    {
        ChatClientInformation? found = null;
        foreach (var (_, users) in _userConnections)
        {
            foreach (var userInformation in users)
            {
                if (userInformation.ConnectionId == Context.ConnectionId)
                {
                    found = userInformation;
                    break;
                }
            }

            if (found != null)
            {
                users.Remove(found);
                break;
            }
        }

    }

}