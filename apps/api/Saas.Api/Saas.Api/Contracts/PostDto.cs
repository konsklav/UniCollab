using Saas.Domain.Posts;

namespace Saas.Api.Contracts;

/// <summary>
/// DTO for a single post. Contains essential metadata.
/// </summary>
/// <param name="Title">The post's title. Length is validated</param>
/// <param name="Content">The post's content. As long as you like.</param>
/// <param name="Subjects">Tags regarding the subjects the post talks about.</param>
/// <param name="Author">The user that uploaded/created the post.</param>
/// <param name="Slug">A URL-friendly name created from the title.</param>
/// <param name="UploadDate">The Date and Time the post was uploaded.</param>
public sealed record PostDto(
    Guid Id,
    string Title,
    string Content,
    List<string> Subjects,
    UserInformationDto Author,
    string Slug,
    DateTime UploadDate)
{
    internal static PostDto From(Post post) =>
        new(Id: post.Id,
            Title: post.Title.Value,
            Content: post.Content,
            Subjects: post.Subjects.Select(s => s.Name).ToList(),
            Author: UserInformationDto.From(post.Author),
            Slug: post.Slug,
            UploadDate: post.CreatedAt);
}