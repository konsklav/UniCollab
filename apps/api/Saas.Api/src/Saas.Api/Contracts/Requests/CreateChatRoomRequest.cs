namespace Saas.Api.Contracts.Requests;

public sealed record CreateChatRoomRequest(string Name, List<UserInformationDto> InitialParticipants);