using Ardalis.Result;

namespace Saas.Domain;

public class User(Guid id, string username, string password, List<User> friends)
{
    private readonly List<User> _friends = friends;
    
    public Guid Id { get; } = id;
    public string Username { get; } = username;
    public string Password { get; } = password;

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
}