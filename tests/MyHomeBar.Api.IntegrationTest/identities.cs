using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MyHomeBar.Api.IntegrationTest
{
    public static class Identities
    {
        public static readonly IEnumerable<Claim> Pepe = new[]
        {
            new Claim(ClaimTypes.Name,"Pepe"),
            new Claim(ClaimTypes.DateOfBirth, "1981-11-07", ClaimValueTypes.Date),
            new Claim(ClaimTypes.Email, "die@nexo.es", ClaimValueTypes.Email),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("IsBanned", "false", ClaimValueTypes.Boolean),
        };

        public static readonly IEnumerable<Claim> Alex = new[]
        {
            new Claim(ClaimTypes.Name,"Alex"),
            new Claim(ClaimTypes.DateOfBirth, "1982-01-07", ClaimValueTypes.Date),
            new Claim(ClaimTypes.Email, "die@nexo.es", ClaimValueTypes.Email),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("IsBanned", "false", ClaimValueTypes.Boolean),
            new Claim("TemporaryBadgeExpiry", DateTime.UtcNow.AddDays(1).ToString("O"), ClaimValueTypes.Date),
        };

        public static readonly IEnumerable<Claim> Victor = new[]
        {
            new Claim(ClaimTypes.Name,"Victor"),
            new Claim(ClaimTypes.DateOfBirth, "2001-11-07", ClaimValueTypes.Date),
            new Claim(ClaimTypes.Email, "die@nexo.es", ClaimValueTypes.Email),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("IsBanned", "false", ClaimValueTypes.Boolean),
            new Claim("TemporaryBadgeExpiry", DateTime.UtcNow.AddDays(1).ToString("O"), ClaimValueTypes.Date),
        };
    }
}
