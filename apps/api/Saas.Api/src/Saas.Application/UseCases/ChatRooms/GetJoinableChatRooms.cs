using Ardalis.Result;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.ChatRooms;

public class GetJoinableChatRooms(IChatRoomRepository roomRepository)
{
    public async Task<Result<List<ChatRoom>>> Handle(Guid userId)
    {
        var chatRooms = await roomRepository.GetJoinableFor(userId);
        return chatRooms;
    }
}