using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Saas.Api.Authentication;

public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
    public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var token))
        {
            return AuthenticateResult.Fail("Missing Authorization Header");
        }

        if (token.Count != 1)
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }

        var tokens = token.ToString().Split(" ");
        
        var scheme = tokens[0];

        if (scheme != "Basic")
        {
            return AuthenticateResult.Fail("Invalid Authorization Scheme");
        }
        
        var encodedCredentials = tokens[1];
        var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials)).Split(':');

        if (decodedCredentials.Length != 2)
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
        
        var username = decodedCredentials[0];
        var password = decodedCredentials[1];
        
        var claims = new List<Claim>();
         claims.Add(new Claim("username", username));
         claims.Add(new Claim("password", password));
         
        var claimsIdentity = new ClaimsIdentity(claims, "Basic");
        
        var principal = new ClaimsPrincipal(claimsIdentity);
        
        var ticket = new AuthenticationTicket(principal, "Basic");
        
        return AuthenticateResult.Success(ticket);
    }
}