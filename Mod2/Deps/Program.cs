using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Deps;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

var builder = Host.CreateDefaultBuilder();
builder.ConfigureAppConfiguration(opts=>{
});

builder.ConfigureLogging(opts=>{
    opts.ClearProviders();
    opts.AddConsole();
    opts.AddEventLog();
});

builder.ConfigureServices(services=>{
    //services.AddTransient<ICounter, Counter>();
    //services.AddScoped<ICounter, Counter>();
    services.AddSingleton<ICounter, Counter>();
    services.AddHostedService<MainApp>();
    services.AddHostedService<MainApp2>();
});

var app = builder.Build();

app.Run();