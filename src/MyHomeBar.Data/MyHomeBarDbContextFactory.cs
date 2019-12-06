using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MyHomeBar.Data
{
    public class MyHomeBarDbContextFactory : IDesignTimeDbContextFactory<MyHomeBarDbContext>
    {
        public MyHomeBarDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .Build();

            var builder = new DbContextOptionsBuilder<MyHomeBarDbContext>();

            var connectionString = configuration.GetConnectionString("MyHomeBarConnection");

            builder.UseSqlServer(connectionString);

            return new MyHomeBarDbContext(builder.Options);
        }
    }
}
