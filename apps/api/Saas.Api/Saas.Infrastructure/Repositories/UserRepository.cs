using Saas.Application.Interfaces;
using Saas.Domain;

namespace Saas.Infrastructure.Repositories;

public class UserRepository : IUserRepository, IUnitOfWork
{
    public Task<User?> GetUserByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetFriendsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}