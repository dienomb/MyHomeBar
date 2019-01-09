using JWTSimpleServer;
using JWTSimpleServer.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyHomeBar.Data.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyHomeBar.Host.Authorization
{
    public class CustomAuthenticationProvider : IAuthenticationProvider
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public CustomAuthenticationProvider(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ValidateClientAuthentication(JwtSimpleServerContext context)
        {
            (bool isSucceed, List<Claim> claims) = await this.ValidateUser(context.UserName, context.Password);
            if (isSucceed)
            {
                context.Success(claims);
            }
            else
            {
                context.Reject("Invalid user authentication");
            }
        }

        private async Task<(bool, List<Claim>)> ValidateUser(string user, string password)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                signInManager = scope.ServiceProvider.GetService<SignInManager<ApplicationUser>>();
                userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                var findUser = await userManager.FindByNameAsync(user);
                var signInResult = await signInManager.CheckPasswordSignInAsync(findUser, password, false);
                List<Claim> claims = new List<Claim>();

                if (signInResult.Succeeded)
                {
                    var roles = await userManager.GetRolesAsync(findUser);
                    claims.Add(new Claim(ClaimTypes.Name, user));
                    claims.Add(new Claim(ClaimTypes.DateOfBirth, findUser.BirthDate.ToString(), ClaimValueTypes.Date));
                    claims.Add(new Claim(ClaimTypes.Email, findUser.Email, ClaimValueTypes.Email));
                    claims.Add(new Claim("IsBanned", findUser.IsBanned.ToString(), ClaimValueTypes.Boolean));
                    claims.Add(new Claim("TemporaryAccessExpiry", findUser.Voucher ?? string.Empty, ClaimValueTypes.Date));
                    claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
                }
                return (signInResult.Succeeded, claims);
            }
        }
    }
}