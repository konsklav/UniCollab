using System.Buffers.Text;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ardalis.Result;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Saas.Application.Authentication;
using Saas.Application.Models;
using Saas.Domain;

namespace Saas.Infrastructure.Authentication;

public class AuthenticationHelper(IConfiguration configuration) : IAuthenticationHelper
{
    public AuthenticationToken GenerateBasicToken(User user)
    {
        var token = $"{user.Username}:{user.Password}";
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var encodedToken = Convert.ToBase64String(tokenBytes);

        return new AuthenticationToken(value: $"Basic {encodedToken}");
    }

    public async Task<Result<AuthenticationTokens>> ParseGoogleToken(string token)
    {
        var jwtKey = configuration["Auth:JwtKey"];
        var jwtIssuer = configuration["Auth:JwtIssuer"];

        if (jwtKey == null || jwtIssuer == null)
            throw new InvalidOperationException("Auth.JwtKey and/or Auth.JwtIssuer are not defined!");

        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(token);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, payload.Subject),
                new Claim(ClaimTypes.Name, payload.Name),
                new Claim(ClaimTypes.Email, payload.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer: jwtIssuer,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            var jwt = $"Bearer {new JwtSecurityTokenHandler().WriteToken(jwtToken)}";
            return new AuthenticationTokens(new AuthenticationToken(jwt), payload);
        }
        catch (InvalidJwtException jwtException)
        {
            return Result.Invalid(new ValidationError(jwtException.Message));
        }
    }
}

