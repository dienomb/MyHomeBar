using JWTSimpleServer.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyHomeBar.Api;
using MyHomeBar.Data;
using MyHomeBar.Data.Identity;
using MyHomeBar.Host.Authorization;
using System;
using System.Reflection;

namespace MyHomeBar.Host
{
    public class Startup
    {
        public const string SigningKey = "InMemorySampleSigningKey";

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; }


        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = currentEnvironment;

            //var optionsBuilder = new DbContextOptionsBuilder<MyHomeBarDbContext>();
            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MyHomeBarConnection"));

            //using (var context = new MyHomeBarDbContext(optionsBuilder.Options))
            //{
            //    context.Database.EnsureCreated();
            //}

        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services)
                 .AddDbContext<MyHomeBarDbContext>(options =>
                 {
                     options.UseSqlServer(Configuration.GetConnectionString("MyHomeBarConnection"), sqlOptions =>
                     {
                         sqlOptions.MigrationsAssembly(typeof(MyHomeBarDbContext).GetTypeInfo().Assembly.GetName().Name);
                     });
                 })
                 .AddIdentity()
                 //.AddScoped<IAuthenticationProvider, CustomAuthenticationProvider>()
                 .AddSingleton<IAuthenticationProvider, CustomAuthenticationProvider>()
                 .AddJwtSimpleServer(setup =>
                  {
                      setup.IssuerSigningKey = SigningKey;
                  })
                .AddJwtInMemoryRefreshTokenStore()
                .AddHomeBarLogging()
                .AddOpenApi();
         }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var services = serviceScope.ServiceProvider;
                    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                    try
                    {
                        services.GetService<MyHomeBarDbContext>().Database.Migrate();
                        var RoleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                        AppIdentityDbContextSeed.SeedAsync(userManager, RoleManager).Wait();
                    }
                    catch (Exception ex)
                    {
                        var logger = loggerFactory.CreateLogger<Program>();
                        logger.LogError(ex, "An error occurred seeding the DB.");
                    }
                }

            ApiConfiguration.Configure(
               app,
               host => host
                   .UseCustomExceptionMiddleware()
                   .UseRouting()
                   .UseSwagger()
                   .UseSwaggerUI(setup =>
                   {
                       setup.SwaggerEndpoint("/swagger/v1/swagger.json", "My Home Bar");
                   })
                   .UseStaticFiles()
                   .UseJwtSimpleServer(setup =>
                    {
                        setup.IssuerSigningKey = SigningKey;
                    })
                   .UseAuthentication()
                   .UseEndpoints(endpoints => { endpoints.MapControllers(); })
           );
        }
    }
}
