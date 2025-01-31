using Ardalis.Result;
using Saas.Domain.Common;

// ReSharper disable ReplaceWithPrimaryConstructorParameter

namespace Saas.Domain;

public class Group(Title name, List<User> members, User creator, Guid? id = null) : Entity(id)
{
    private readonly List<User> _members = members;

    public Title Name { get; private set; } = name;
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