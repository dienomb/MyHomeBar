using Microsoft.AspNetCore.Authorization;
using MyHomeBar.Domain.Entities;

namespace MyHomeBar.Authorization.Requirements
{
    public class AccessContentRequirement : IAuthorizationRequirement
    {
        public AccessContentRequirement(Scale scale)
        {
            this.scale = scale;
        }

        public Scale scale { get; }
    }
}
