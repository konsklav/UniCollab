using Ardalis.Result;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases;

public class GetChatRoomUseCase(IChatRoomRepository roomRepository)
{
    public async Task<Result<ChatRoom>> Handle(Guid chatRoomId)
    {
        var chatRoom = await roomRepository.GetByIdAsync(chatRoomId);
        if (chatRoom is null) 
            return Result<ChatRoom>.NotFound();

        return chatRoom;
    }
}