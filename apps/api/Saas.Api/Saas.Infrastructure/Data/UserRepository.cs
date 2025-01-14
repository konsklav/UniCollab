using Saas.Application.Interfaces;
using Saas.Domain;

namespace Saas.Infrastructure.Data;

public class UserRepository : IUserRepository
{
    public Task<User?> GetUserByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetFriendsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }
}