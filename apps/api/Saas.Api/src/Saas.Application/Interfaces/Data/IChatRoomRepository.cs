using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface IChatRoomRepository : IUnitOfWork
{
    Task<List<ChatRoom>> GetJoinableFor(Guid userId);
    Task<List<ChatRoom>> GetByUserAsync(Guid userId);
    Task<ChatRoom?> GetByIdAsync(Guid chatId);
    Task<IReadOnlyList<Message>?> GetMessagesAsync(Guid chatId);
    
    Task<List<ChatRoom>> GetMutualChatsOf(Guid userId1, Guid userId2);

    void Add(ChatRoom room);
}