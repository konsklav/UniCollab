using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface IUserRepository : IUnitOfWork
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid userId);
    Task<List<User>> GetByIdsAsync(List<Guid> userIds);
    Task<User?> GetByBasicCredentialsAsync(string username, string password);
    Task<User?> GetByGoogleCredentialsAsync(string email, string googleId);

    Task<User?> GetByUsernameAsync(string username);
    Task<bool> GoogleIdExistsAsync(string googleId);

    void Add(User user);
}