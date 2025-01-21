using Ardalis.Result;
using Saas.Domain.Common;

namespace Saas.Domain.Posts;

public class Post : Entity
{
    private Post() {}
    private Post(
        Title title, 
        string content, 
        List<Subject> subjects, 
        User author, 
        string slug, 
        DateTime createdAt,
        Guid? id = null) : base(id)
    {
        Title = title;
        Content = content;
        Subjects = subjects;
        Author = author;
        Slug = slug;
        CreatedAt = createdAt;
    }

    public Title Title { get; private set; }
    public string Content { get; private set; }
    public string Slug { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IReadOnlyList<Subject> Subjects { get; private set; }
    public User Author { get; private set; }

    public static Result<Post> Create(string title, string content, List<Subject> subjects, User author)
    {
        var titleResult = Title.Create(title);
        if (!titleResult.IsSuccess)
            return titleResult.Map();

        var validTitle = titleResult.Value;
        
        return new Post(
            validTitle, 
            content,
            subjects, 
            author, 
            SlugHelper.Get(validTitle.Value), 
            DateTime.UtcNow);
    }
}