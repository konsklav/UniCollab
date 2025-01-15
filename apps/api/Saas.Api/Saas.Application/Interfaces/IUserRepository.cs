using Saas.Domain;

namespace Saas.Application.Interfaces;

public interface IUserRepository : IUnitOfWork
{
    Task<User?> GetUserByIdAsync(Guid id);
}