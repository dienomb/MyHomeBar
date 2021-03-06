﻿
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MyHomeBar.Api.IntegrationTest.Infrastructure
{
    public class MyTestsAuthenticationHandler
         : AuthenticationHandler<MyTestOptions>
    {
        public MyTestsAuthenticationHandler(IOptionsMonitor<MyTestOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"HttpAPITesting"),
                new Claim(ClaimTypes.DateOfBirth, "1971-12-20", ClaimValueTypes.Date),
                new Claim(ClaimTypes.Email, "die@nexo.es", ClaimValueTypes.Email),
                new Claim(ClaimTypes.Role, "Vendor"),
                new Claim("IsBanned", "false", ClaimValueTypes.Boolean),
                new Claim("TemporaryAccessExpiry", DateTime.UtcNow.AddDays(1).ToString("O"), ClaimValueTypes.Date),
            };

            ClaimsIdentity identity = new ClaimsIdentity(
               claims: claims,
               authenticationType: Scheme.Name,
               nameType: ClaimTypes.Name,
               roleType: ClaimTypes.Role);

            AuthenticationTicket ticket = new AuthenticationTicket(
                new ClaimsPrincipal(identity),
                new AuthenticationProperties(),
                Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public class MyTestOptions
        : AuthenticationSchemeOptions
    {
    }
}
