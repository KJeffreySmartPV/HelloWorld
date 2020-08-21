﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Logging;
namespace HelloWorld.WebApp.Stateful
{
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables().SetBasePath(Directory.GetCurrentDirectory()).Build();

            var builder = WebHost.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(configure => configure.ValidateScopes = false)
                .UseConfiguration(config)
                .UseStartup<Startup>();
            builder.ConfigureLogging((hostingContext, loggingBuilder) =>
            {
                loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDynamicConsole();
            });
            return builder;
        }
    }
}
