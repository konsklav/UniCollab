using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.ChatRooms;

public class AddChatMessageToRoom(IChatRoomRepository repository) : IApplicationUseCase
{
    public async Task<Result> Handle(Guid chatRoomId, Message message)
    {
        var chatRoom = await repository.GetByIdAsync(chatRoomId);

        if (chatRoom == null)
            return Result.NotFound($"Couldn't find chat room with id: {chatRoomId}");
        
        var result = chatRoom.AddMessage(message);

        await repository.SaveChangesAsync();
        
        return result;
    }
}