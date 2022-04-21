using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Deps
{
    public class MainApp : IHostedService
    {
         private IServiceProvider _serviceProvider;
         private ILogger<MainApp> _logger;
         private IConfiguration _config;

        public MainApp(IServiceProvider serviceProvider, ILogger<MainApp> logger, IConfiguration config)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _config = config;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
           _logger.LogDebug("MainApp is started");
           //var data = _config.GetValue<string>("MySettings:Item1");
           var data = _config.GetSection("MySettings").Get<MySettings>();
           System.Console.WriteLine(data.Item1);
            using (var scope = _serviceProvider.CreateScope())
            {
                ICounter _counter = scope.ServiceProvider.GetRequiredService<ICounter>();
                _counter.Increment();
                 _counter = scope.ServiceProvider.GetRequiredService<ICounter>();
                _counter.Increment();
                 _counter = scope.ServiceProvider.GetRequiredService<ICounter>();
                _counter.Increment();
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
           return Task.CompletedTask;
        }
    }
}