using Saas.Domain;

namespace Saas.Application.Authentication;

public interface IAuthenticationHelper
{
    AuthenticationToken GenerateToken(User user);
}