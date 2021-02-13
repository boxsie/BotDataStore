using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BotData.Api
{
    public static class Program
    {
        public static string CurrentEnvironment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public static void Main(string[] args)
        {
            BuildWebHost(args, CurrentEnvironment).Run();
        }

        public static IWebHost BuildWebHost(string[] args, string environment)
        {
            var configuration = CreateConfigurationBuilder(args, environment)
                .Build();

            var webHost = CreateWebHostBuilder(configuration)
                .Build();

            return webHost;
        }

        public static IConfigurationBuilder CreateConfigurationBuilder(string[] args, string environment)
        {
            return new ConfigurationBuilder()
                .AddApplicationPath(environment)
                .AddApplicationSettings(environment)
                .AddCommandLine(args);
        }

        public static IWebHostBuilder CreateWebHostBuilder(IConfiguration configuration)
        {
            return new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseUrls(configuration.Get<AppSettings>().Endpoint)
                .UseConfiguration(configuration)
                .UseStartup<Startup>();
        }

        public static IConfigurationBuilder AddApplicationPath(this IConfigurationBuilder config, string environment)
        {
            if (!string.IsNullOrEmpty(environment))
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
            }

            return config;
        }

        public static IConfigurationBuilder AddApplicationSettings(this IConfigurationBuilder config, string environment, string basePath = "")
        {
            config.AddJsonFile(Path.Join(basePath, "appsettings.json"), false, true);

            if (!string.IsNullOrEmpty(environment) && environment != "Local")
            {
                config.AddJsonFile(Path.Join(basePath, $"appsettings.{environment}.json"), false, true);
            }

            return config;
        }
    }
}
