using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace MyHomeBar.Data.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                var adminUser = new ApplicationUser { UserName = "admin", Email = "die@nexo.es", BirthDate = new DateTime(1981, 11, 07), IsBanned = false };
                var result = await userManager.CreateAsync(adminUser, "admin");
                if (result.Succeeded)
                {
                    createRole(roleManager, "Admin").Wait();
                    var currentUser = await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                var guestUser = new ApplicationUser { UserName = "guest", Email = "guest@nexo.es", BirthDate = new DateTime(1982, 03, 07), IsBanned = false };
                result = await userManager.CreateAsync(guestUser, "guest");
                if (result.Succeeded)
                {
                    createRole(roleManager, "Guest").Wait();
                    var currentUser = await userManager.AddToRoleAsync(guestUser, "Guest");
                }

                var vendorUser = new ApplicationUser { UserName = "vendor", Email = "vendor@nexo.es", BirthDate = new DateTime(1985, 08, 07), IsBanned = false };
                result = await userManager.CreateAsync(vendorUser, "vendor");
                if (result.Succeeded)
                {
                    createRole(roleManager, "Vendor").Wait();
                    var currentUser = await userManager.AddToRoleAsync(vendorUser, "Vendor");
                }

                var minorUser = new ApplicationUser { UserName = "minor", Email = "minor@nexo.es", BirthDate = new DateTime(2010, 08, 07), IsBanned = false };
                result = await userManager.CreateAsync(minorUser, "minor");
                if (result.Succeeded)
                {
                    createRole(roleManager, "Admin").Wait();
                    var currentUser = await userManager.AddToRoleAsync(minorUser, "Admin");
                }

                var bannedUser = new ApplicationUser { UserName = "banned", Email = "banned@nexo.es", BirthDate = new DateTime(1985, 08, 07), IsBanned = true };
                result = await userManager.CreateAsync(bannedUser, "banned");
                if (result.Succeeded)
                {
                    createRole(roleManager, "Vendor").Wait();
                    var currentUser = await userManager.AddToRoleAsync(vendorUser, "Vendor");
                }

                var voucherUser = new ApplicationUser { UserName = "voucher", Email = "voucher@nexo.es", BirthDate = new DateTime(1985, 08, 07), Voucher = DateTime.UtcNow.AddDays(1).ToString("O"), IsBanned = false };
                result = await userManager.CreateAsync(voucherUser, "voucher");
                if (result.Succeeded)
                {
                    createRole(roleManager, "Guest").Wait();
                    var currentUser = await userManager.AddToRoleAsync(vendorUser, "Guest");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static async Task createRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            bool x = await roleManager.RoleExistsAsync(roleName);
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = roleName;
                await roleManager.CreateAsync(role);
            }
        }
    }
}