using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyHomeBar.Api.IntegrationTest.Infrastructure
{
    public class TestHostFixture : IDisposable
    {
        public TestHostFixture()
        {
            var host = new WebHostBuilder()
                .UseStartup<TestStartup>();

            Server = new TestServer(host);
        }

        public TestServer Server { get; }

        public void Dispose()
        {
            Server.Dispose();
        }
    }
}
