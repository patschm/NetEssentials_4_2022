using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace TheClient;

class Program
{
    //static HttpClient client = new HttpClient {BaseAddress = new Uri("https://localhost:7071/")};
    private static IHost app;

    //static AppContex
    static void Main()
    {
        var builder = Host.CreateDefaultBuilder();
        builder.ConfigureServices(services=>{
            services.AddHttpClient("datasvc", opts=>{
                opts.BaseAddress = new Uri("https://localhost:7071/");
            }).SetHandlerLifetime(TimeSpan.FromSeconds(5));
        });
        app = builder.Build();

        ReadData();
        Console.ReadLine();
    }

    private static async void ReadData()
    {
        IHttpClientFactory fact = app.Services.GetRequiredService<IHttpClientFactory>();
        for(int i = 0; i < 3000; i++)
        {
            var client = fact.CreateClient("datasvc");
            // using(HttpClient client = new HttpClient())
            // {
            //     client.BaseAddress = new Uri("https://localhost:7071/");
                Task.Delay(100).Wait();
                var response = await client.GetAsync("weatherforecast");
                System.Console.WriteLine(response.StatusCode);
                //System.Console.WriteLine(response.Content.Headers.ContentType);
                string data = await response.Content.ReadAsStringAsync();
                //System.Console.WriteLine(data);
            //}
        }
    }
}
