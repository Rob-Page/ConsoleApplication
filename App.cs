using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ConsoleApplication;
using System;

namespace ConsoleApplication.Settings
{
    public class App
    {
        private readonly IConfigurationRoot _config;
        private readonly ILogger<App> _logger;
        private readonly IServiceProvider _serviceProvider;

        public App(IConfigurationRoot config, ILoggerFactory loggerFactory, IServiceProvider serviceProvider )
        {
            _logger = loggerFactory.CreateLogger<App>();
            _serviceProvider = serviceProvider;
            _config = config;
        }

        public async Task Run()
        {
            await new Menu(_logger,_serviceProvider ).DisplayMenuOptions();
        }
    }
}