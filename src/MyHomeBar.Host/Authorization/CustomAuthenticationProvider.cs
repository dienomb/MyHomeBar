using JWTSimpleServer;
using JWTSimpleServer.Abstractions;
using Microsoft.AspNetCore.Identity;
using MyHomeBar.Data.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyHomeBar.Host.Authorization
{
    public class CustomAuthenticationProvider : IAuthenticationProvider
    {
        private UserManager<ApplicationUser> _userManager = null;
        private SignInManager<ApplicationUser> _signInManager = null;


        public CustomAuthenticationProvider(, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

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