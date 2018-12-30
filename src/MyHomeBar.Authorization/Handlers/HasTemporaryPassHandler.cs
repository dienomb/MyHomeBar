using Microsoft.AspNetCore.Authorization;
using MyHomeBar.Authorization.Requirements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeBar.Authorization.Handlers
{
    public class HasTemporaryPassHandler : AuthorizationHandler<TemporaryPermissionRequirement>
    {
        private const string ExpectedIssuer = "LOCAL AUTHORITY";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TemporaryPermissionRequirement requirement)
        {
            var temporaryBadgeClaim = context.User.FindFirst(c => c.Type == "TemporaryBadgeExpiry");

            if (temporaryBadgeClaim == null)
            {
                return Task.CompletedTask;
            }

            // The issuer of the TemporaryBadgeClaim claim should be known
            if (temporaryBadgeClaim.Issuer != ExpectedIssuer)
            {
                return Task.CompletedTask;
            }

            var temporaryBadgeExpiry = Convert.ToDateTime(temporaryBadgeClaim.Value).ToUniversalTime();

            if (temporaryBadgeExpiry > DateTime.UtcNow)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
