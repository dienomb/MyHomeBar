using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace MyHomeBar.Api.IntegrationTest
{
    public static class Identities
    {
        public static readonly IEnumerable<Claim> Menor = new[]
       {
            new Claim(ClaimTypes.Name,"Pepe"),
            new Claim(ClaimTypes.DateOfBirth, "2018-11-07", ClaimValueTypes.Date),
            new Claim(ClaimTypes.Email, "die@nexo.es", ClaimValueTypes.Email),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("IsBanned", "false", ClaimValueTypes.Boolean),
        };

        public static readonly IEnumerable<Claim> IsBanned = new[]
       {
            new Claim(ClaimTypes.Name,"Pepe"),
            new Claim(ClaimTypes.DateOfBirth, "1981-11-07", ClaimValueTypes.Date),
            new Claim(ClaimTypes.Email, "die@nexo.es", ClaimValueTypes.Email),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("IsBanned", "true", ClaimValueTypes.Boolean),
        };

        public static readonly IEnumerable<Claim> TemporaryPass = new[]
        {
            new Claim(ClaimTypes.Name,"Victor"),
            new Claim(ClaimTypes.DateOfBirth, "1982-11-07", ClaimValueTypes.Date),
            new Claim(ClaimTypes.Email, "die@nexo.es", ClaimValueTypes.Email),
            new Claim(ClaimTypes.Role, "Vendor"),
            new Claim("IsBanned", "false", ClaimValueTypes.Boolean),
            new Claim("TemporaryAccessExpiry", DateTime.UtcNow.AddDays(1).ToString("O"), ClaimValueTypes.Date),
        };

        public static readonly IEnumerable<Claim> Admin = new[]
        {
            new Claim(ClaimTypes.Name,"Pepe"),
            new Claim(ClaimTypes.DateOfBirth, "1981-11-07", ClaimValueTypes.Date),
            new Claim(ClaimTypes.Email, "die@nexo.es", ClaimValueTypes.Email),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("IsBanned", "false", ClaimValueTypes.Boolean),
        };

        public static readonly IEnumerable<Claim> Guest = new[]
        {
            new Claim(ClaimTypes.Name,"Alex"),
            new Claim(ClaimTypes.DateOfBirth, "1982-01-07", ClaimValueTypes.Date),
            new Claim(ClaimTypes.Email, "alex@nexo.es", ClaimValueTypes.Email),
            new Claim(ClaimTypes.Role, "Guest"),
            new Claim("IsBanned", "false", ClaimValueTypes.Boolean),
        };

        public static readonly IEnumerable<Claim> Vendor = new[]
        {
            new Claim(ClaimTypes.Name,"Victor"),
            new Claim(ClaimTypes.DateOfBirth, "1982-11-07", ClaimValueTypes.Date),
            new Claim(ClaimTypes.Email, "die@nexo.es", ClaimValueTypes.Email),
            new Claim(ClaimTypes.Role, "Vendor"),
            new Claim("IsBanned", "false", ClaimValueTypes.Boolean),
        };
    }
}
