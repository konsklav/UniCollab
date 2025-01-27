using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using Saas.Domain.Common;

namespace Saas.Domain;

/// <summary>
/// Contains information about the message, when it was sent and who sent it. 
/// </summary>
public class Message : Entity
{
    private Message() { }

    public Message(string content, DateTime sentAt, User sender, Guid? id = null) : base(id)
    {
        Content = content;
        SentAt = sentAt;
        Sender = sender;
    }

    public string Content { get; private set; }
    public DateTime SentAt { get; private set; }
    public User Sender { get; private set; }
}