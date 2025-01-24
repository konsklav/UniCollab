using Ardalis.Result;
using Saas.Domain.Common;
using Saas.Domain.Posts;

// ReSharper disable ReplaceWithPrimaryConstructorParameter

namespace Saas.Domain;

public class ChatRoom : Entity
{
    private readonly List<User> _participants;
    private readonly List<Message> _messages;

    private ChatRoom() { }
    private ChatRoom(Title name, List<User> participants, List<Message> messages, Guid? id = null) : base(id)
    {
        _participants = participants;
        _messages = messages;
        Name = name;
    }

    public Title Name { get; private set; }
    public IReadOnlyList<User> Participants => _participants;
    public IReadOnlyList<Message> Messages => _messages;

    public static Result<ChatRoom> Create(string name, List<User> participants)
    {
        var errors = new List<ValidationError>();
        
        var titleResult = Title.Create(name);
        if (!titleResult.IsSuccess)
            errors.AddRange(titleResult.ValidationErrors);

        if (participants.Count == 0)
            errors.Add(new ValidationError("Cannot create a chat room with no participants."));

        if (errors.Count > 0)
            return Result.Invalid(errors);

        return new ChatRoom(
            name: titleResult.Value,
            participants: participants,
            messages: []);
    }
    
    public Result AddParticipant(User user)
    {
        if (Participants.Contains(user))
            return Result.Conflict();
        
        _participants.Add(user);
        return Result.Success();
    }
    
    public Result RemoveParticipant(User user)
    {
        if (!Participants.Contains(user))
            return Result.NotFound();
        
        _participants.Remove(user);
        return Result.Success();
    }
    
    public Result AddMessage(Message message)
    {
        if (Messages.Contains(message))
            return Result.Conflict();
        
        _messages.Add(message);
        return Result.Success();
    }

    public Result DeleteMessage(Message message)
    {
        if (!Messages.Contains(message))
            return Result.NotFound();
        
        _messages.Remove(message);
        return Result.Success();
    }
}