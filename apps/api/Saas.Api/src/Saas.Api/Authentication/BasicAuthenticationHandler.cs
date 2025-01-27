using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Saas.Application.Authentication;

#pragma warning disable CS0618 // Type or member is obsolete

namespace Saas.Api.Authentication;

internal sealed class BasicAuthenticationHandler(
    IOptionsMonitor<BasicAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock)
    : AuthenticationHandler<BasicAuthenticationOptions>(options, logger, encoder, clock)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var token))
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));

        if (token.Count != 1)
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));

        var tokens = token.ToString().Split(" ");

        if (tokens.Length != 2)
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Form"));
        
        var scheme = tokens[0];

        if (scheme != "Basic")
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Scheme"));
        
        var encodedCredentials = tokens[1];
        var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials)).Split(':');

        if (decodedCredentials.Length != 2)
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        
        var username = decodedCredentials[0];
        var password = decodedCredentials[1];
        
        var claims = new List<Claim>
        {
            new("username", username),
            new("password", password)
        };

        var claimsIdentity = new ClaimsIdentity(claims, UniCollabAuthSchemes.Basic);
        var principal = new ClaimsPrincipal(claimsIdentity);
        var ticket = new AuthenticationTicket(principal, UniCollabAuthSchemes.Basic);
        
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}