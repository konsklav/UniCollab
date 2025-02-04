namespace Saas.Api.Contracts.Requests;

public sealed record CreateGroupRequest(string Name, Guid CreatorId, List<Guid> InitialMembers);