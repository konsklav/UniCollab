using Ardalis.Result;
using Saas.Application.Models;
using Saas.Domain;

namespace Saas.Application.Authentication;

public interface IAuthenticationHelper
{
    AuthenticationToken GenerateBasicToken(User user);
    Task<Result<AuthenticationTokens>> ParseGoogleToken(string token);
}