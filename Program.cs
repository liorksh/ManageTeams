using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreWebService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // using NuGet package: Microsoft.Extensions.Configuration.CommandLine
            // Build the config from the command line arguments
            var config = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();
            
            int port = 5011;
            // receive the port as an input
            port = config.GetValue<int>("port", port);
            // receive the whole URL as an input
            string url = config.GetValue<string>("url");

            // the URL can be *, not necessarily localhost. It allows flexibility in deploying it in any platform/host.
            url = String.IsNullOrEmpty(url) ? $"http://*:{port}/" : url;
            
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseUrls(url)
                .Build();            

            host.Run();

            Console.WriteLine($"Host is up and running in URL: {url}");
        }
    }
}
