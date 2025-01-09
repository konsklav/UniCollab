namespace Saas.Domain;

public class Message(Guid id, string content, DateTime sentAt, User sender)
{
    public Guid Id { get; } = id;
    public string Content { get; } = content;
    public DateTime SentAt { get; } = sentAt;
    public User Sender { get; } = sender;
}