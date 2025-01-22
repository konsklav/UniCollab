using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Infrastructure.Repositories;

internal class ChatRepository(UniCollabContext context) : IChatRepository
{
    public Task<List<ChatRoom>> GetAllAsync()
    {
        // Θα είναι έτσι αν προσθέσουμε DbSet Chatroom στο context, κλπ
        //return await context.ChatRooms.ToListAsync();
        throw new NotImplementedException();
    }  

    public Task<ChatRoom?> GetByIdAsync(Guid chatRoomId)
    {
        // Θα είναι έτσι αν προσθέσουμε DbSet Chatroom στο context, κλπ
        //var chatRoom = await context.ChatRooms
        //    .Include(c => c.Participants)
        //    .Include(c => c.Messages)
        //    .FirstOrDefaultAsync(u => u.Id == chatRoomId);
            
        //return chatRoom;
        throw new NotImplementedException();
    }
    
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}