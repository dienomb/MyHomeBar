using Microsoft.AspNetCore.Authorization;
using MyHomeBar.Authorization.Requirements;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyHomeBar.Authorization.Handlers
{
    public class IsAdminOrGuestHandler : AuthorizationHandler<CanViewAndServeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanViewAndServeRequirement requirement)
        {
            var roleClaim = context.User.FindFirst(ClaimTypes.Role);

            if (roleClaim == null)
            {
                return Task.CompletedTask;
            }

            if (roleClaim.Value == "Admin")
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
