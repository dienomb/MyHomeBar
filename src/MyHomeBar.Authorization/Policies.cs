using Microsoft.AspNetCore.Authorization;
using MyHomeBar.Authorization.Requirements;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyHomeBar.Authorization
{
    public static class Policies
    {
        public const string Over18Years = "Over18Years";
        public const string TemporaryPermission = "TemporaryPermission";
        public const string CheckIfBanned = "IsNotBanned";
        public const string CanAddDrinks = "CanAddDrinks";
        public const string CanMakeParties = "CanMakeParties";
        public const string CanViewAndServe = "CanViewAndServe";

         public static void Configure(AuthorizationOptions options)
        {
            // Code based policy: Calculations over Claims information
            options.AddPolicy(Over18Years, builder =>
            {
                builder.AddRequirements(new MinimumAgeRequirement(18));
            });

            options.AddPolicy(TemporaryPermission, policyBuilder =>
            {
                policyBuilder.AddRequirements(new TemporaryPermissionRequirement());
            });

            options.AddPolicy(CheckIfBanned, policyBuilder =>
            {
                policyBuilder.AddRequirements(new CheckIfBannedRequirement());
            });

            // Policy with simple rules. Based in User claims information
            options.AddPolicy(CanAddDrinks, policyBuilder =>
            {
                policyBuilder.RequireRole("Admin", "Vendor");
            });

            options.AddPolicy(CanViewAndServe, policyBuilder =>
            {
                policyBuilder.RequireRole("Admin", "Guest");
            });

            options.AddPolicy(CanMakeParties, policyBuilder =>
            {
                policyBuilder.RequireRole("Admin");
            });
        }
    }
}
