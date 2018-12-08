using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace MyHomeBar.Host
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
           .AddEnvironmentVariables()
           .Build();

        public static int Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                //.Enrich.FromLogContext()
                //.WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Getting the motors running...");
                Log.Information("Args: {a}", args);

                Log.ForContext<Program>().Information("Hello, world!");
                Log.ForContext<Program>().Error("Hello, world!");      
                Log.ForContext(Constants.SourceContextPropertyName, "Microsoft").Warning("Hello, world!");
                Log.ForContext(Constants.SourceContextPropertyName, "Microsoft").Error("Hello, world!");
                Log.ForContext(Constants.SourceContextPropertyName, "MyApp.Something.Tricky").Verbose("Hello, world!");

                Log.Information("Destructure with max object nesting depth:\n{@NestedObject}",
                    new { FiveDeep = new { Two = new { Three = new { Four = new { Five = "the end" } } } } });

                Log.Information("Destructure with max string length:\n{@LongString}",
                    new { TwentyChars = "0123456789abcdefghij" });

                Log.Information("Destructure with max collection count:\n{@BigData}",
                    new { TenItems = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" } });

                Log.Information("Destructure with policy to strip password:\n{@LoginData}",
                    new LoginData { Username = "BGates", Password = "isityearoflinuxyet" });

                BuildWebHost(args).Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseConfiguration(Configuration)
                   .UseStartup<Startup>()
                   .UseSerilog()
                   .Build();
    }
}
