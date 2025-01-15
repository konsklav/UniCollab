using Saas.Application.Interfaces.Events;
using Saas.Domain;

namespace Saas.Application.UseCases;

public class AddChatMessageToRoom
{
    public async Task Handle(Guid chatRoomId, Message message)
    {
        throw new NotImplementedException();
    }
}