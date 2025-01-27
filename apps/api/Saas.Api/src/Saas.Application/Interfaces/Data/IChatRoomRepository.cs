using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface IChatRoomRepository : IUnitOfWork
{
    Task<List<ChatRoom>> GetJoinableFor(Guid userId);
    Task<ChatRoom?> GetByIdAsync(Guid chatRoomId);
    void Add(ChatRoom room);
}