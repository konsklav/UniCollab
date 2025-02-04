using Saas.Domain;

namespace Saas.Api.Contracts.Requests;

public sealed record CreatePostRequest(string Title, string Content, List<string> Subjects);