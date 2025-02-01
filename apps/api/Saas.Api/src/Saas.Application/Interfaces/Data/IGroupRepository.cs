using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface IGroupRepository : IUnitOfWork
{
    Task<Group?> GetByIdAsync(Guid groupId);
    void Add(Group group);
}