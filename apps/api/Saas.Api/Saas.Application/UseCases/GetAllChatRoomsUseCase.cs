using Ardalis.Result;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases;

public class GetAllChatRoomsUseCase(IChatRepository repository)
{
    public async Task<Result<List<ChatRoom>>> Handle()
    {
        var chatRooms = await repository.GetAllAsync();
        return chatRooms;
    }
}