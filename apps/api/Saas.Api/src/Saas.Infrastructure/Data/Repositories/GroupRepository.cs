using Microsoft.EntityFrameworkCore;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Infrastructure.Data.Repositories;

internal class GroupRepository(UniCollabContext context) : IGroupRepository
{
    public async Task<Group?> GetByIdAsync(Guid groupId)
    {
        var group = await context.Groups
            .AsSingleQuery()
            .Include(g => g.Members)
            .Include(g => g.Creator)
            .FirstOrDefaultAsync(g => g.Id == groupId);
        return group;
    }
    
    public void Add(Group group) => context.Groups.Add(group);
    
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}