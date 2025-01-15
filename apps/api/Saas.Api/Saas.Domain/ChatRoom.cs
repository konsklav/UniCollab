using Ardalis.Result;
using Saas.Domain.Common;

// ReSharper disable ReplaceWithPrimaryConstructorParameter

namespace Saas.Domain;

public class ChatRoom(string name, string type, List<User> participants, List<Message> messages, Guid? id = null) : Entity(id)
{
    private readonly List<User> _participants = participants;
    private readonly List<Message> _messages = messages;
    
    public string Name { get; } = name;
    public string Type { get; } = type;
    public IReadOnlyList<User> Participants => _participants;
    public IReadOnlyList<Message> Messages => _messages;

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
}