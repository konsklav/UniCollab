using Saas.Domain;

namespace Saas.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<List<User>> GetFriendsAsync(Guid userId);

    Task UpdateAsync(User user);
}