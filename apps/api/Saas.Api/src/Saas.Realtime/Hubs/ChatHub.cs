using Microsoft.AspNetCore.SignalR;
using Saas.Application.Common.Events;
using Saas.Application.Contracts;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Realtime;
using Saas.Application.UseCases.ChatRooms;
using Saas.Realtime.Contracts;

namespace Saas.Realtime.Hubs;

public class ChatHub(IEventService eventService, AddChatMessageToRoom addMessage) : Hub<IChatClient>
{
    public async Task JoinChat(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }

    public async Task LeaveChat(string chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
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
            SenderConnectionId: Context.ConnectionId,
            ChatId: chatGuid);


        await Clients.Group(serverMessage.ChatId).ReceiveMessage(MessageDto.From(message));
        await eventService.PublishAsync(@event);
    }
}