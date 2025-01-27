namespace Saas.Realtime.Contracts;

public record ServerMessage(string ChatId, string UserId, string Content);