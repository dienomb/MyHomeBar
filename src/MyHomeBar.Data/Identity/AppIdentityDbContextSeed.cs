using Microsoft.AspNetCore.Identity;
using MyHomeBar.Data.Identity;
using System.Threading.Tasks;

namespace MyHomeBar.Data.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
            await userManager.CreateAsync(defaultUser, "Pass@word1");
        }
    }
}