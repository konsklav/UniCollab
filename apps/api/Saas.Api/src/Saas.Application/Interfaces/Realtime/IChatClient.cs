using Saas.Application.Contracts;

namespace Saas.Application.Interfaces.Realtime;

public interface IChatClient
{
    Task ReceiveMessage(MessageDto message);
}