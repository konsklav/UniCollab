namespace Saas.Application.Common.Authentication;

public class AuthenticationToken(string value)
{
    public string Value { get; } = value;
}