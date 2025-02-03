namespace Saas.Application.Contracts;

public sealed record NotificationDto(string Type, string Header, string Message);