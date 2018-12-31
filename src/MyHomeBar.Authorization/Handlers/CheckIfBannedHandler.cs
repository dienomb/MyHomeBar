using Microsoft.AspNetCore.Authorization;
using MyHomeBar.Authorization.Requirements;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeBar.Authorization.Handlers
{
    public class CheckIfBannedHandler : AuthorizationHandler<CheckIfBannedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckIfBannedRequirement requirement)
        {
            var nameIdentifierClaim = context.User.FindFirst(c => c.Type == "IsBanned");

            if (nameIdentifierClaim == null)
            {
                return Task.CompletedTask;
            }

            var isBanned = Convert.ToBoolean(nameIdentifierClaim.Value);
            if (isBanned)
            {
                context.Fail();
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
