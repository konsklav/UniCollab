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

    public async Task<List<Group>> GetByUserAsync(Guid userId)
    {
        var groups = await context.Groups
            .Include(c => c.Members)
            .Where(c => c.Members.Any(u => u.Id == userId))
            .ToListAsync();
        return groups;
    }

    public Task<List<Group>> GetJoinableFor(Guid userId)
    {
        var groups = context.Groups
            .Include(c => c.Members)
            .Where(c => c.Members.Any(u => u.Id != userId))
            .ToListAsync();
        return groups;
    }

    public void Add(Group group) => context.Groups.Add(group);
    
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}