using Microsoft.AspNetCore.Authorization;
using MyHomeBar.Authorization.Requirements;
using MyHomeBar.Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyHomeBar.Authorization.Handlers
{
    public class AccessContentHandler : AuthorizationHandler<AccessContentRequirement, Drink>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AccessContentRequirement requirement,
            Drink resource)
        {
            var roleClaim = context.User.FindFirst(ClaimTypes.Role);

            if (roleClaim == null)
            {
                return Task.CompletedTask;
            }

            if (roleClaim.Value == "Guest" && resource.Scale == requirement.scale)
            {
                return Task.CompletedTask;
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
