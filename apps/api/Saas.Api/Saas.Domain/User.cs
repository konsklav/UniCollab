namespace Saas.Domain;

public class User(Guid id, string username, string password, List<User> friends)
{
    public Guid Id { get; } = id;
    public string Username { get; } = username;
    public string Password { get; } = password;
    public List<User> Friends { get; } = friends;
}