using Microsoft.AspNetCore.Authorization;
using MyHomeBar.Authorization.Requirements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeBar.Authorization.Handlers
{
    public class IsBannedAuthorizationHandler : AuthorizationHandler<IsNotBannedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsNotBannedRequirement requirement)
        {
            if (context.User.HasClaim(claim => claim.Type == "IsBannedFromDrinking"))
            {
                context.Fail();
            }
            return Task.FromResult(0);
        }
    }
}
