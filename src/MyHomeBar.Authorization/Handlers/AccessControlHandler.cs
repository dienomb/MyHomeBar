using Microsoft.AspNetCore.Authorization;
using MyHomeBar.Authorization.Requirements;
using MyHomeBar.Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyHomeBar.Authorization.Handlers
{
    public class AccessControlHandler : AuthorizationHandler<AccessControlRequirement, Drink>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AccessControlRequirement requirement,
            Drink resource)
        {
            var nameIdentifierClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim == null)
            {
                return Task.CompletedTask;
            }

            if (resource.Name == nameIdentifierClaim.Value)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
