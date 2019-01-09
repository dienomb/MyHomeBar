using Microsoft.AspNetCore.Authorization;
using MyHomeBar.Domain.Entities;

namespace MyHomeBar.Authorization.Requirements
{
    public class AccessContentRequirement : IAuthorizationRequirement
    {
        public AccessContentRequirement(Level level)
        {
            this.level = level;
        }

        public Level level { get; }
    }
}
