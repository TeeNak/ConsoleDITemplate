using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleTemplate1
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var app = serviceProvider.GetService<Application>();
                Task.Run(() => app.Run()).Wait();
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {

            services.AddLogging(builder => builder.AddConsole());

            IConfigurationRoot configuration = GetConfiguration();
            services.AddSingleton<IConfiguration>(configuration);
            
            services.AddTransient<Application>();
        }

        private static IConfigurationRoot GetConfiguration()
        {

            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

    }
}
