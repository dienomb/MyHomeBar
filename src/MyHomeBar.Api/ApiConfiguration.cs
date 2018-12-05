using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MyHomeBar.Api.Filters;
using System;

namespace MyHomeBar.Api
{
    public class ApiConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            return services
                    .AddMvcCore(config => config.Filters.Add(typeof(ValidModelStateFilter)))
                    .AddJsonFormatters()
                    .AddApiExplorer()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ValidModelStateFilter>())
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .Services;
        }

        public static IApplicationBuilder Configure(IApplicationBuilder app, Func<IApplicationBuilder, IApplicationBuilder> configureHost)
        {
            return configureHost(app).UseMvc();
        }
    }
}
