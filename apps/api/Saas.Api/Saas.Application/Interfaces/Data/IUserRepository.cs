using Saas.Domain;
using Saas.Domain.Posts;

namespace Saas.Application.Interfaces.Data;

public interface IUserRepository : IUnitOfWork
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid userId);
}