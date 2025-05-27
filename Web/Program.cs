using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Persistense.Provider;
using System.Threading.Tasks;

namespace Web;

public class Program
{
    public async static Task Main(string[] args)
    {
        IHost host = CreateHostBuilder(args).ConfigureAppConfiguration((_, configuration) => configuration.AddCustomConfiguration()).Build();

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}
