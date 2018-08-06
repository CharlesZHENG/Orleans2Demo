using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;
using Contracts;

namespace Silo
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var siloBuilder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .UseDashboard(options => { })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "Orleans2DependencyInjection";
                })
                .Configure<EndpointOptions>(options =>
                    options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ITimeService, TimeService>();
                    services.AddSingleton<ICommentRepository, MemoryCommentRepository>();
                })
                .ConfigureLogging(logging => logging.AddConsole());

            using (var host = siloBuilder.Build())
            {
                await host.StartAsync();

                Console.ReadLine();
            }
        }
    }
}
