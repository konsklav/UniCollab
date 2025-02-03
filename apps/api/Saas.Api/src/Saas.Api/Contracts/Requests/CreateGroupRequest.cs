namespace Saas.Api.Contracts.Requests;

public sealed record CreateGroupRequest(string Name, List<Guid> InitialMembers);