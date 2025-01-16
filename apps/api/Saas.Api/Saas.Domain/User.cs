using Ardalis.Result;
using Saas.Domain.Common;

// ReSharper disable ReplaceWithPrimaryConstructorParameter

namespace Saas.Domain;

public class User : Entity
{
    private readonly List<User> _friends;
    private readonly List<Post> _posts;

    private User() {} // It's never used but it's mandatory for EF-Core!
    
    public User(string username, string password, List<User> friends, List<Post> posts, Guid? id = null) : base(id)
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

    public Result JoinChat(ChatRoom chatRoom)
    {
        var result = chatRoom.AddParticipant(this);
        return result;
    }

    public Result SendMessage(ChatRoom chatRoom,Message message)
    {
        var result = chatRoom.AddMessage(message);
        return result;
    }
    
    public Result LeaveChat(ChatRoom chatRoom)
    {
        var result = chatRoom.RemoveParticipant(this);
        return result;
    }

    public Result CreatePost(string title, string content, List<Subject> subjects)
    {
        var post = new Post(title, content, subjects, this);
        _posts.Add(post);
        return Result.Success();
    }

    public Result<Group> CreateGroup(string name)
    {
        var group = new Group(
            name: name, 
            members: [this], 
            creator: this);

        return group;
    }
}