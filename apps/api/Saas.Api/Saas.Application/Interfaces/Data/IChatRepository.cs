using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface IChatRepository : IUnitOfWork
{
    Task<List<ChatRoom>> GetAllAsync();
    Task<ChatRoom?> GetByIdAsync(Guid chatRoomId);
}