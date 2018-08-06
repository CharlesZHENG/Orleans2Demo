using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Orleans2GettingStarted
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var siloBuilder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .UseDashboard(options => { })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "Orleans2GettingStarted";
                })
                .Configure<EndpointOptions>(options =>
                    options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureLogging(logging => logging.AddConsole());

            using (var host = siloBuilder.Build())
            {
                await host.StartAsync();

                var clientBuilder = new ClientBuilder()
                    .UseLocalhostClustering()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "Orleans2GettingStarted";
                    })
                    .ConfigureLogging(logging => logging.AddConsole());

                using (var client = clientBuilder.Build())
                {
                    await client.Connect();

                    var random = new Random();
                    string sky = "blue";

                    while (sky == "blue") // if run in Ireland, it exits loop immediately
                    {
                        int grainId = random.Next(0, 500);
                        double temperature = random.NextDouble() * 40;
                        var sensor = client.GetGrain<ITemperatureSensorGrain>(grainId);
                        await sensor.SubmitTemperatureAsync((float)temperature);
                    }
                }
            }
        }
    }
}
