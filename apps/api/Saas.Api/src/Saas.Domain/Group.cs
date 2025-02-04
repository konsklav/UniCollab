using Ardalis.Result;
using Saas.Domain.Common;

// ReSharper disable ReplaceWithPrimaryConstructorParameter

namespace Saas.Domain;

public class Group : Entity
{
    private readonly List<User> _members;

    private Group() { }
    private Group(Title name, List<User> members, User creator, Guid? id = null) : base(id)
    {
        _members = members;
        Name = name;
        Creator = creator;
    }

    public Title Name { get; private set; }
    public IReadOnlyList<User> Members => _members;
    public User? Creator { get; private set; }
    
    public static Result<Group> Create(string name, List<User> members, User creator)
    {
        var errors = new List<ValidationError>();
        
        var titleResult = Title.Create(name);
        if (!titleResult.IsSuccess)
            errors.AddRange(titleResult.ValidationErrors);

        if (members.Count == 0)
            errors.Add(new ValidationError("Cannot create a chat room with no participants."));

        if (errors.Count > 0)
            return Result.Invalid(errors);

        return new Group(
            name: titleResult.Value,
            members: members,
            creator: creator);
    }

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