using Saas.Domain.Common;

namespace Saas.Domain;

public class Post(string title, string content, List<Subject> subjects, User author, Guid? id = null) : Entity(id)
{
    public string Title { get; } = title;
    public string Content { get; } = content;
    public List<Subject> Subjects { get; } = subjects;
    public User Author { get; } = author;
}