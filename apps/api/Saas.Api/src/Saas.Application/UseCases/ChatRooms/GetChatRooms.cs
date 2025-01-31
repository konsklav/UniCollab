using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.ChatRooms;

public class GetChatRooms(IChatRoomRepository roomRepository) : IApplicationUseCase
{
    public async Task<Result<List<ChatRoom>>> Joinable(Guid userId)
    {
        var chatRooms = await roomRepository.GetJoinableFor(userId);
        return chatRooms;
    }
    
    public async Task<Result<List<ChatRoom>>> Participating(Guid userId)
    {
        throw new NotImplementedException();
    }
}