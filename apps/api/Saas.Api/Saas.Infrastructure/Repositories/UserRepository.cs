using Microsoft.EntityFrameworkCore;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Infrastructure.Repositories;

internal class UserRepository(UniCollabContext context) : IUserRepository
{
    public async Task<List<User>> GetAllAsync() => await context.Users.ToListAsync();

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        var user = await context.Users
            .Include(u => u.Friends)
            .Include(u => u.Posts)
            .FirstOrDefaultAsync(u => u.Id == userId);
            
        return user;
    }
    
    public Task<List<User>?> GetByIdsAsync(List<Guid> userIds)
    {
        var users = context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();
        
        return users;
    }

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}