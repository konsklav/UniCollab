using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.ChatRooms;

public class GetChatRoom(IChatRoomRepository roomRepository) : IApplicationUseCase
{
    public async Task<Result<ChatRoom>> Handle(Guid chatRoomId)
    {
        var chatRoom = await roomRepository.GetByIdAsync(chatRoomId);
        if (chatRoom is null) 
            return Result<ChatRoom>.NotFound();

        return chatRoom;
    }
}