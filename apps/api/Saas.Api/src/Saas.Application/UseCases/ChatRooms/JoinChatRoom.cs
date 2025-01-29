using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;

namespace Saas.Application.UseCases.ChatRooms;

public class JoinChatRoom(
    IUserRepository userRepository,
    IChatRoomRepository chatRepository) : IApplicationUseCase
{
    public async Task<Result> HandleAsync(Guid chatId, Guid userId)
    {
        var user = await userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result.NotFound($"User with ID '{userId}' not found.");

        var chat = await chatRepository.GetByIdAsync(chatId);
        if (chat is null)
            return Result.NotFound($"Chat with ID '{chatId}' not found.");

        var joinResult = user.JoinChat(chat);
        return joinResult;
    }
}