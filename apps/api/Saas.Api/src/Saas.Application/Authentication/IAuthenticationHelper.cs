using System.Security.Claims;
using Saas.Application.Common.Authentication;
using Saas.Domain;

namespace Saas.Application.Interfaces.Authentication;

public interface IAuthenticationHelper
{
    AuthenticationToken GenerateToken(User user);
}