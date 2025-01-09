namespace Saas.Domain;

public class Group(Guid id, string name, List<User> members, User creator)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public List<User> Members { get; } = members;
    public User Creator { get; } = creator;
}