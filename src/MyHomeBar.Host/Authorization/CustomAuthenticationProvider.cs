using JWTSimpleServer;
using JWTSimpleServer.Abstractions;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyHomeBar.Host.Authorization
{
    public class CustomAuthenticationProvider : IAuthenticationProvider
    {
        public Task ValidateClientAuthentication(JwtSimpleServerContext context)
        {
            if (context.UserName == "demo" && context.Password == "demo")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "demo")
                };

                context.Success(claims);
            }
            else
            {
                context.Reject("Invalid user authentication");
            }

            return Task.CompletedTask;
        }
    }
}