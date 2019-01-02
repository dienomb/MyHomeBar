using JWTSimpleServer.Abstractions;
using Microsoft.AspNetCore.Identity;
using MyHomeBar.Data;
using MyHomeBar.Data.Identity;
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

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>(options => { });
            new IdentityBuilder(typeof(ApplicationUser), typeof(IdentityRole), services)
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddEntityFrameworkStores<MyHomeBarDbContext>();
            return services;

        }
    }
}
