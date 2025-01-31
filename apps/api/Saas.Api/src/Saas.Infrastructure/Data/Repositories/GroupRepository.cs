using Microsoft.EntityFrameworkCore;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Infrastructure.Data.Repositories;

internal class GroupRepository(UniCollabContext context) : IGroupRepository
{
    public async Task<Group?> GetByIdAsync(Guid groupId)
    {
        // var group = await context.Groups
        //         //Not done yet, needs includes!
        //     .FirstOrDefaultAsync(g => g.Id == groupId);
        throw new NotImplementedException();
    }
    
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}