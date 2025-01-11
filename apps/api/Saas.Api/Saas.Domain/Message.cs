namespace Saas.Domain;

/// <summary>
/// Contains information about the message, when it was sent and who sent it. 
/// </summary>
public record Message(string Content, DateTime SentAt, User Sender);