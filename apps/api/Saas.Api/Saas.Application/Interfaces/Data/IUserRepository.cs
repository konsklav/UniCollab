using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface IUserRepository : IUnitOfWork
{
    Task<User?> GetUserByIdAsync(Guid id);
}