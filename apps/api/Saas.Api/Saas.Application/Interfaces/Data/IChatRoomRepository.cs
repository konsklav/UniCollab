using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface IChatRoomRepository : IUnitOfWork
{
    Task<List<ChatRoom>> GetAllAsync();
    Task<ChatRoom?> GetByIdAsync(Guid chatRoomId);
    Task AddAsync(ChatRoom room);
}