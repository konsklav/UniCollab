using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.ChatRooms;

public class GetChatRooms(IChatRoomRepository chatRepository) : IApplicationUseCase
{
    /// <summary>
    /// Gets chat rooms that user specified by <paramref name="userId"/> can join.
    /// </summary>
    public async Task<Result<List<ChatRoom>>> Joinable(Guid userId)
    {
        var chatRooms = await chatRepository.GetJoinableFor(userId);
        return chatRooms;
    }
    
    /// <summary>
    /// Gets chat rooms that user specified by <paramref name="userId"/> is a part of.
    /// </summary>
    public async Task<Result<List<ChatRoom>>> Participating(Guid userId)
    {
        var participatingChatRooms = await chatRepository.GetByUserAsync(userId);
        return participatingChatRooms;
    }
}