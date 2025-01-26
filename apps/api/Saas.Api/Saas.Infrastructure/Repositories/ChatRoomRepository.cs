using Microsoft.EntityFrameworkCore;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Infrastructure.Repositories;

internal class ChatRoomRepository(UniCollabContext context) : IChatRoomRepository
{
    public async Task<List<ChatRoom>> GetJoinableFor(Guid userId)
    {
        return await context.ChatRooms
            .Include(c => c.Participants)
            .Where(c => c.Participants.All(p => p.Id != userId))
            .ToListAsync();
    }
    
    public async Task<ChatRoom?> GetByIdAsync(Guid chatRoomId)
    {
        var chatRoom = await context.ChatRooms
            .AsSplitQuery()
            .Include(c => c.Participants)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(u => u.Id == chatRoomId);
            
        return chatRoom;
    }

    public void Add(ChatRoom room)
    {
        // Simple! 
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}