using JWTSimpleServer.Abstractions;
using MyHomeBar.Host.Authorization;
using MyHomeBar.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationProvider, CustomAuthenticationProvider>();
            return services;
        }
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
            Serilog.ILogger log = Serilog.Log.Logger;

            services
                .AddSingleton<ILogger>(sp => 
                {
                    return new SerilogAdapter(log);
                });
            return services;
        }
    }
}
