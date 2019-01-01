using Microsoft.AspNetCore.Authorization;
using MyHomeBar.Authorization.Requirements;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyHomeBar.Authorization.Handlers
{
    public class CheckIfBannedHandler : AuthorizationHandler<CheckIfBannedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckIfBannedRequirement requirement)
        {
            Claim nameIdentifierClaim = context.User.FindFirst(c => c.Type == "IsBanned");

            if (nameIdentifierClaim == null)
            {
                return Task.CompletedTask;
            }

            bool isBanned = Convert.ToBoolean(nameIdentifierClaim.Value);
            if (isBanned)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
