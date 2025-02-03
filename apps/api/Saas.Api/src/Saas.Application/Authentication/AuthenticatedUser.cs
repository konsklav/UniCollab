using Saas.Domain;

namespace Saas.Application.Authentication;

public class AuthenticatedUser(User user, AuthenticationToken token)
{
    public User User { get; } = user;
    public AuthenticationToken Token { get; } = token;
}