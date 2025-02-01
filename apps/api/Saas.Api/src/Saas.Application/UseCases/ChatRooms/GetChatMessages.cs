using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.ChatRooms;

public class GetChatMessages(IChatRoomRepository repository) : IApplicationUseCase
{
    public async Task<Result<IReadOnlyList<Message>>> HandleAsync(Guid chatId)
    {
        var messages = await repository.GetMessagesAsync(chatId);
        if (messages == null)
            return Result.NotFound($"Chat with ID: '{chatId}' does not exist");

        return Result.Success(messages);
    }
}