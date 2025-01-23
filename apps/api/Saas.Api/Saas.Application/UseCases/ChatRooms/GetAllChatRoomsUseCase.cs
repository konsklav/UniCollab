using Ardalis.Result;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases;

public class GetAllChatRoomsUseCase(IChatRoomRepository roomRepository)
{
    public async Task<Result<List<ChatRoom>>> Handle()
    {
        var chatRooms = await roomRepository.GetAllAsync();
        return chatRooms;
    }
}