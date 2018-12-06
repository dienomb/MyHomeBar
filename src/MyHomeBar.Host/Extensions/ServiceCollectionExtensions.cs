using Microsoft.Extensions.DependencyInjection;
using MyHomeBar.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.DescribeAllParametersInCamelCase();
                setup.DescribeStringEnumsInCamelCase();
                setup.SwaggerDoc("v1", new Info
                {
                    Title = "My home Bar",
                    Version = "v1"
                });
            });

            return services;
        }

        public static IServiceCollection AddHomeBarLogging(this IServiceCollection services)
        {
            var log = services.BuildServiceProvider();
            var test = log.GetService<Serilog.ILogger>();
            services
                .AddSingleton<ILogger>(sp => 
                {
                    return new SerilogAdapter(sp.GetService<Serilog.ILogger>());
                });
            return services;
        }
    }
}
