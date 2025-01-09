using Ardalis.Result;

namespace Saas.Domain;

public class Group(Guid id, string name, List<User> members, User creator)
{
    private readonly List<User> _members = members;
    
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public IReadOnlyList<User> Members => _members;
    public User Creator { get; } = creator;

    public Result AddMember(User user)
    {
        if (Members.Contains(user))
            return Result.Conflict();
        
        _members.Add(user);
        return Result.Success();
    }

    public Result RemoveMember(User user)
    {
        if (!Members.Contains(user))
            return Result.NotFound();
        
        _members.Remove(user);
        return Result.Success();
    }
}