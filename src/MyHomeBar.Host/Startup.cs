using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyHomeBar.Api;

namespace MyHomeBar.Host
{
    public class Startup
    {
        public const string SigningKey = "InMemorySampleSigningKey";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services)
                  .AddJwtSimpleServer(setup =>
                  {
                      setup.IssuerSigningKey = SigningKey;
                  })
                .AddJwtInMemoryRefreshTokenStore()
                .AddHomeBarLogging()
                .AddOpenApi();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            ApiConfiguration.Configure(
               app,
               host => host
                   .UseCustomExceptionMiddleware()
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
                   .UseHttpsRedirection()
           );
        }
    }
}
