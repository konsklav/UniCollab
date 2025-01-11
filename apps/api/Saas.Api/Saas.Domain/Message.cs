using Saas.Domain.Common;

namespace Saas.Domain;

/// <summary>
/// Contains information about the message, when it was sent and who sent it. 
/// </summary>
public class Message(string content, DateTime sentAt, User sender, Guid? id = null) : Entity(id)
{
    public string Content { get; private set; } = content;
    public DateTime SentAt { get; private set; } = sentAt;
    public User Sender { get; private set; } = sender;
}