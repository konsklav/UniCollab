namespace Saas.Domain;

public class ChatRoom(Guid id, string name, string type, List<User> participants, List<Message> messages)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public string Type { get; } = type;
    public List<User> Participants { get; } = participants;
    public List<Message> Messages { get; } = messages;
}