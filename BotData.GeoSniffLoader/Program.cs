using System;
using System.Threading.Tasks;
using BotData.Data;
using BotData.Data.Context;
using CommandLine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BotData.GeoSniffLoader
{
    internal class Program
    {
        private static Opts _opts;

        private static async Task Main(string[] args)
        {
            Parser.Default.ParseArguments<Opts>(args).WithParsed(x => _opts = x);

            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection, _opts);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            if (serviceProvider == null)
                throw new Exception("Could not create service provider");

            var app = serviceProvider.GetService<App>();

            if (app == null)
                throw new Exception("Could not create app");

            await app.RunAsync(_opts);
        }

        private static void ConfigureServices(IServiceCollection services, Opts options)
        {
            services.AddTransient<App>();

            var dbCreds = DbCreds.GetCreds();

            services.AddDbContext<BotDataContext>(builder =>
            {
                builder.UseNpgsql(dbCreds.ConnectionString);
            });
        }
    }
}