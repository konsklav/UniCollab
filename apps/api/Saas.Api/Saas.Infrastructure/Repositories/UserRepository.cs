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

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await db.Users
            .Include(u => u.Friends)
            .FirstOrDefaultAsync(u => u.Id == id);
            
        return user;
    }
    
    public async Task SaveChangesAsync()
    {
        await db.SaveChangesAsync();
    }
}