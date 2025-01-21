using Ardalis.Result;
using Saas.Domain.Common;

namespace Saas.Domain;

public class Post : Entity
{
    private Post() {}
    private Post(string title, string content, List<Subject> subjects, User author, string slug, Guid? id = null) : base(id)
    {
        Title = title;
        Content = content;
        Subjects = subjects;
        Author = author;
        Slug = slug;
    }

    public string Title { get; private set; }
    public string Content { get; private set; }
    public string Slug { get; private set; }
    public IReadOnlyList<Subject> Subjects { get; private set; }
    public User Author { get; private set; }

    public static Result<Post> Create(string title, string content, List<Subject> subjects, User author)
    {
        return new Post(title, content, subjects, author, SlugHelper.Get(title));
    }
}