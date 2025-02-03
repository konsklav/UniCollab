using Ardalis.Result;
using Saas.Domain.Common;

// ReSharper disable ReplaceWithPrimaryConstructorParameter

namespace Saas.Domain;

public class User : Entity
{
    private readonly List<User> _friends;
    private readonly List<Post> _posts;

    public const int MaxUsernameLength = 32;
    public const int MaxPasswordLength = 128;

    private User() {} // It's never used but it's mandatory for EF-Core!
    private User(string username, string password, List<User> friends, List<Post> posts, Guid? id = null) : base(id)
    {
        _friends = friends;
        _posts = posts;
        Username = username;
        Password = password;
    }

    public string Username { get; private set; }
    public string Password { get; private set; }

    public IReadOnlyList<User> Friends => _friends;

    public IReadOnlyList<Post> Posts => _posts;

    public static Result<User> Create(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return Result.Invalid(new ValidationError("Username and/or password cannot be empty."));

        if (username.Length > MaxUsernameLength)
            return Result.Invalid(new ValidationError($"Username can be no more than {MaxUsernameLength} letters."));

        if (password.Length > MaxPasswordLength)
            return Result.Invalid(new ValidationError($"Password can be no more than {MaxPasswordLength} letters."));

        return new User(
            username,
            password,
            friends: [],
            posts: []);
    }

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
    
    public Result JoinGroup(Group group) => group.AddMember(this);
    public Result LeaveGroup(Group group) => group.RemoveMember(this);

    public Result JoinChat(ChatRoom chatRoom) => chatRoom.AddParticipant(this);
    public Result LeaveChat(ChatRoom chatRoom) => chatRoom.RemoveParticipant(this);

    public Result SendMessage(ChatRoom chatRoom, string content)
    {
        var message = new Message(content, DateTime.UtcNow, this);
        return chatRoom.AddMessage(message);
    }

    public Result CreatePost(string title, string content, List<Subject> subjects)
    {
        var post = Post.Create(title, content, subjects, this);
        _posts.Add(post);
        return Result.Success();
    }
}