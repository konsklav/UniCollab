using Saas.Realtime.Contracts;

namespace Saas.Realtime.Clients;

public interface IChatClient
{
    Task ReceiveMessage(ClientMessage message);
}