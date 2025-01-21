using Saas.Domain.Posts;

namespace Saas.Api.Contracts;

public sealed record PostDto(
    string Title,
    string Content,
    List<string> Subjects,
    string AuthorUsername,
    string Slug,
    DateTime UploadDate)
{
    public static PostDto From(Post post) =>
        new(Title: post.Title.Value,
            Content: post.Content,
            Subjects: post.Subjects.Select(s => s.Name).ToList(),
            AuthorUsername: post.Author.Username,
            Slug: post.Slug,
            UploadDate: post.CreatedAt);
}