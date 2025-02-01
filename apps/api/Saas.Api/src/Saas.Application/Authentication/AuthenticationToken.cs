namespace Saas.Application.Authentication;

public class AuthenticationToken(string value)
{
    public string Value { get; } = value;
}