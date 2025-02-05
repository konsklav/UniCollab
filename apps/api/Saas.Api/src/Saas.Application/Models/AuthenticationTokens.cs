using Google.Apis.Auth;
using Saas.Application.Authentication;

namespace Saas.Application.Models;

public sealed record AuthenticationTokens(AuthenticationToken ApplicationToken, GoogleJsonWebSignature.Payload GooglePayload);
