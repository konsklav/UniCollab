using Ardalis.Result;
using Saas.Domain.Common;

// ReSharper disable ReplaceWithPrimaryConstructorParameter

namespace Saas.Domain;

public class User : Entity
{
    private readonly List<User> _friends;

    private User() {} // It's never used but it's mandatory for EF-Core!
    
    public User(string username, string password, List<User> friends, Guid? id = null) : base(id)
    {
        _friends = friends;
        Username = username;
        Password = password;
    }

    public string Username { get; private set; }
    public string Password { get; private set; }

    public IReadOnlyList<User> Friends => _friends;

    public Result AddFriend(User user)
    {
        if (user == this)
            return Result.Invalid();

        if (Friends.Contains(user))
            return Result.Conflict();

        _friends.Add(user);
        return Result.Success();
    }

    public Result RemoveFriend(User user)
    {
        if (!Friends.Contains(user))
            return Result.NotFound();
                        
        _friends.Remove(user);
        return Result.Success();
    }

    // !!! From here on I'm not sure about the implementation. Let's discuss them next time. !!!
    public Result JoinGroup(Group group)
    {
        var result = group.AddMember(this);
        return result;
    }

    public Result LeaveGroup(Group group)
    {
        var result = group.RemoveMember(this);
        return result;
    }
}