using Saas.Application.Interfaces;
using Saas.Application.UseCases;
using Saas.Application.UseCases.ChatRooms;
using Saas.Application.UseCases.Users;
using Saas.Domain;

namespace Saas.Application.Common.Events;

internal sealed class ChatMessageSentEventHandler
{
    private readonly AddChatMessageToRoom _addToChat;
    private readonly GetUserUseCase _getUser;
    
    public ChatMessageSentEventHandler(
        IEventService eventService, 
        AddChatMessageToRoom addToChat,
        GetUserUseCase getUser)
    {
        _addToChat = addToChat;
        _getUser = getUser;
        
        eventService.Subscribe<ChatMessageSentEvent>(HandleMessageSent);
    }

    private async Task HandleMessageSent(ChatMessageSentEvent sentEvent)
    {
        var getUserResult = await _getUser.Handle(sentEvent.SenderId);
        if (!getUserResult.IsSuccess)
            return;

        var user = getUserResult.Value;
        var message = new Message(
            content: sentEvent.Message,
            sentAt: sentEvent.SentAt,
            sender: user);

        await _addToChat.Handle(sentEvent.ChatId, message);
    }
}