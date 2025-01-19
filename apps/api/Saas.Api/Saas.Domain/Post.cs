using Saas.Domain.Common;

namespace Saas.Domain;

public class Post : Entity
{
    private Post() {}
    public Post(string title, string content, List<Subject> subjects, User author, Guid? id = null) : base(id)
    {
        Title = title;
        Content = content;
        Subjects = subjects;
        Author = author;
    }

    public string Title { get; private set; }
    public string Content { get; private set; }
    public IReadOnlyList<Subject> Subjects { get; private set; }
    public User Author { get; private set; }
}