using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.ChatRooms;

public class CreateChatRoomUseCase(
    IUserRepository userRepository,
    IChatRoomRepository chatRoomRepository) : IApplicationUseCase
{
    public async Task<Result<ChatRoom>> Handle(string name, List<Guid> userIds)
    {
        var users = await userRepository.GetByIdsAsync(userIds);
        if (users is null)
            return Result.NotFound("One or more users do not exist.");

        var chatRoomResult = ChatRoom.Create(name, users);
        if (!chatRoomResult.IsSuccess)
            return chatRoomResult;

        var chatRoom = chatRoomResult.Value;

        chatRoomRepository.Add(chatRoom);
        await chatRoomRepository.SaveChangesAsync();

        return chatRoom;
    }
}