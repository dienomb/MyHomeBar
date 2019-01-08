using JWTSimpleServer;
using JWTSimpleServer.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyHomeBar.Data.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyHomeBar.Host.Authorization
{
    public class CustomAuthenticationProvider : IAuthenticationProvider
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<IdentityRole> roleManager;

        public CustomAuthenticationProvider(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ValidateClientAuthentication(JwtSimpleServerContext context)
        {
            if (await this.ValidateUser(context.UserName, context.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, context.UserName),
                    new Claim(ClaimTypes.DateOfBirth, "1981-11-07", ClaimValueTypes.Date),
                    new Claim(ClaimTypes.Email, "die@nexo.es", ClaimValueTypes.Email),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim("IsBanned", "false", ClaimValueTypes.Boolean),
                };

                context.Success(claims);
            }
            else
            {
                context.Reject("Invalid user authentication");
            }
        }

        private async Task<bool> ValidateUser(string user, string password)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                signInManager = scope.ServiceProvider.GetService<SignInManager<ApplicationUser>>();
                userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var findUser = await userManager.FindByNameAsync(user);
                var result = await signInManager.CheckPasswordSignInAsync(findUser, password, false);
                return result.Succeeded;
            }
        }
    }
}