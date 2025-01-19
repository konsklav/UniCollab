using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface IUserRepository : IUnitOfWork
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
}