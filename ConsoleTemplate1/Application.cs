using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTemplate1
{
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
