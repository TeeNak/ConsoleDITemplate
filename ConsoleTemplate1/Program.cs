using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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


        public class Application
        {
            ILogger _logger;
            IConfiguration _config;

            public Application(ILogger<Application> logger, IConfiguration config)
            {
                _logger = logger;
                _config = config;
            }

            public async Task Run()
            {
                try
                {

                    var name = _config.GetValue<string>("myoptions:name");

                    _logger.LogError($"This is a console application ");
                    _logger.LogInformation($"This is a console application for {name}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                }
            }
        }
    }
}
