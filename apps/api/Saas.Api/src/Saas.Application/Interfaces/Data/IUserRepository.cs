using System.Linq.Expressions;
using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface IUserRepository : IUnitOfWork
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid userId);
    Task<List<User>?> GetByIdsAsync(List<Guid> userIds);
    Task<User?> GetByCredentialsAsync(string username, string password);

    Task<User?> GetByUsernameAsync(string username);

    void Add(User user);
}