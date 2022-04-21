using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Deps
{
    public class MainApp2 : IHostedService
    {
         private ICounter _counter;
         private IServiceProvider _serviceProvider;

        public MainApp2(ICounter counter, IServiceProvider serviceProvider)
        {
            _counter = counter;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            System.Console.WriteLine("Hallo Svc2");
         
        using(var scope = _serviceProvider.CreateScope())
        {
             _counter = scope.ServiceProvider.GetRequiredService<ICounter>();
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