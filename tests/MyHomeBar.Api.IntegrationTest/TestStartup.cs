using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyHomeBar.Api.IntegrationTest.Infrastructure;


namespace MyHomeBar.Api.IntegrationTest
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services)
                .AddAuthentication(defaultScheme: "TestServer")
                .AddScheme<MyTestOptions, MyTestsAuthenticationHandler>("TestServer", _ => { });
        }

        public void Configure(IApplicationBuilder app)
        {
            ApiConfiguration.Configure(app, host => host.UseAuthentication());
        }
    }
}
