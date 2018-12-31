using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyHomeBar.Api.Filters;
using MyHomeBar.Api.TestRepository;
using MyHomeBar.Authorization;
using MyHomeBar.Authorization.Handlers;
using System;

namespace MyHomeBar.Api
{
    public class ApiConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDrinksRepository, InMemoryProductsRepository>();

            services.AddSingleton<IAuthorizationHandler, HasTemporaryPassHandler>();
            services.AddSingleton<IAuthorizationHandler, CheckIfBannedHandler>();
            services.AddSingleton<IAuthorizationHandler, AccessContentHandler>();

            return services
                    .AddMvcCore(config => config.Filters.Add(typeof(ValidModelStateFilter)))
                    .AddJsonFormatters()
                    .AddApiExplorer()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ValidModelStateFilter>())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddAuthorization(Policies.Configure)
                    .Services;
            // Authorization handlers
        }

        public static IApplicationBuilder Configure(IApplicationBuilder app, Func<IApplicationBuilder, IApplicationBuilder> configureHost)
        {
            return configureHost(app).UseMvc();
        }
    }
}
