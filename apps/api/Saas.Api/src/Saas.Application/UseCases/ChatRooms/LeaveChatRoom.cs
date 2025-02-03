using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Application.UseCases.Users;

namespace Saas.Application.UseCases.ChatRooms;

public class LeaveChatRoom(
    GetUserUseCase getUser,
    GetChatRoom getChat,
    IChatRoomRepository chatRepository) : IApplicationUseCase
{
    public async Task<Result> HandleAsync(Guid chatId, Guid userId)
    {
        var getUserResult = await getUser.Handle(userId);
        if (!getUserResult.IsSuccess)
            return getUserResult.Map();

        var user = getUserResult.Value;
        
        var getChatResult = await getChat.Handle(chatId);
        if (!getChatResult.IsSuccess)
            return getChatResult.Map();

        var chat = getChatResult.Value;

        var leaveResult = user.LeaveChat(chat);
        if (!leaveResult.IsSuccess)
            return leaveResult;

        await chatRepository.SaveChangesAsync();
        return leaveResult;
    }
}