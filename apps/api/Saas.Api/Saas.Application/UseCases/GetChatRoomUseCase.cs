using Ardalis.Result;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases;

public class GetChatRoomUseCase(IChatRepository repository)
{
    public async Task<Result<ChatRoom>> Handle(Guid chatRoomId)
    {
        var chatRoom = await repository.GetByIdAsync(chatRoomId);
        if (chatRoom is null) 
            return Result<ChatRoom>.NotFound();

        return chatRoom;
    }
}