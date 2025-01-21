using Microsoft.EntityFrameworkCore;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Infrastructure.Repositories;

internal class UserRepository(UniCollabContext db) : IUserRepository
{
    public async Task<List<User>> GetAllAsync()
    {
        return await db.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        var user = await db.Users
            .Include(u => u.Friends)
            .Include(u => u.Posts)
            .FirstOrDefaultAsync(u => u.Id == userId);
            
        return user;
    }
    
    public async Task SaveChangesAsync()
    {
        await db.SaveChangesAsync();
    }
}