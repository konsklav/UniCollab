namespace Saas.Domain;

public class Post(Guid id, string title, string content, List<Subject> subjects, User author)
{
    public Guid Id { get; } = id;
    public string Title { get; } = title;
    public string Content { get; } = content;
    public List<Subject> Subjects { get; } = subjects;
    public User Author { get; } = author;
}