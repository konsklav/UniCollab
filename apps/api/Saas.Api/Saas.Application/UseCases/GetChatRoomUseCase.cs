using Ardalis.Result;
using Saas.Domain;

namespace Saas.Application.UseCases;

public class GetChatRoomUseCase
{
    public async Task<Result<ChatRoom>> Handle(Guid chatId)
    {
        throw new NotImplementedException();
    }
}