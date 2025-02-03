using Ardalis.Result;
using Microsoft.VisualBasic.CompilerServices;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Application.UseCases.Users;
using Saas.Domain;

namespace Saas.Application.UseCases.ChatRooms;

public class AddChatMessageToRoom(
    GetUserUseCase getUser,
    IChatRoomRepository chatRepository) : IApplicationUseCase
{
    public async Task<Result<Message>> Handle(Guid chatRoomId, Guid senderId, string content)
    {
        var sentAt = DateTime.UtcNow;
        
        var userResult = await getUser.Handle(senderId);
        if (!userResult.IsSuccess)
            return userResult.Map();
        
        var chatRoom = await chatRepository.GetByIdAsync(chatRoomId);
        if (chatRoom is null)
            return Result.NotFound($"Couldn't find chat room with id: {chatRoomId}");

        var message = new Message(
            content: content,
            sentAt: sentAt,
            sender: userResult.Value);
        
        var result = chatRoom.AddMessage(message);
        if (!result.IsSuccess)
            return result;

        await chatRepository.SaveChangesAsync();
        return message;
    }
}