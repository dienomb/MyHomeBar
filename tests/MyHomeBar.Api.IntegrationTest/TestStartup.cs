using Acheve.AspNetCore.TestHost.Security;
using Acheve.TestHost;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MyHomeBar.Api.IntegrationTest
{
    public class TestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services)
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = TestServerDefaults.AuthenticationScheme;
                })
              .AddTestServer();
            //.AddAuthentication(defaultScheme: "TestServer")
            //.AddScheme<MyTestOptions, MyTestsAuthenticationHandler>("TestServer", _ => { });
        }

        public void Configure(IApplicationBuilder app)
        {
            ApiConfiguration.Configure(app, host => host.UseAuthentication());
        }
    }
}
