using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface IGroupRepository : IUnitOfWork
{
    Task<Group?> GetByIdAsync(Guid groupId);
    Task<List<Group>> GetByUserAsync(Guid userId);
    Task<List<Group>> GetJoinableFor(Guid userId);
    void Add(Group group);
}