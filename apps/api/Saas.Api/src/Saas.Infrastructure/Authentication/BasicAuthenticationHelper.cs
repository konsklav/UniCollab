using System.Buffers.Text;
using System.Diagnostics;
using System.Text;
using Saas.Application.Common.Authentication;
using Saas.Application.Interfaces.Authentication;
using Saas.Domain;

namespace Saas.Infrastructure.Authentication;

public class BasicAuthenticationHelper : IAuthenticationHelper
{
    public AuthenticationToken GenerateToken(User user)
    {
        var token = $"{user.Username}:{user.Password}";
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var encodedToken = Convert.ToBase64String(tokenBytes);

        return new AuthenticationToken(value: $"Basic {encodedToken}");
    }
}