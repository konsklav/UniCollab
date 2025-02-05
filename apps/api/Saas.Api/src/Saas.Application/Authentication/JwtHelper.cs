using Microsoft.Extensions.Configuration;

namespace Saas.Application.Authentication;

public class JwtHelper(IConfiguration configuration)
{
   public JwtInfo GetInfo()
   {
      var key = configuration["Auth:JwtKey"];
      var issuer = configuration["Auth:JwtIssuer"];
      
      return new JwtInfo(key, issuer);
   }
}