using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeamMngtWS.Log;

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

            // initialize the applicative logger
            ILogger logger = InitializeLogger(config);

            // initialize the web host
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .ConfigureServices(log=>
                    log.AddSingleton<ILogger>(logger)) // set the applicative logger
                .ConfigureServices(collection=> 
                    collection.AddSingleton<IConfiguration>(config)) // send the command line arguments to Startup instance
                .UseStartup<Startup>()
                .UseUrls(url)
                .Build();

             Console.WriteLine($"Host is up and running in URL: {url}");

            host.Run();
        }

        private static ILogger InitializeLogger(IConfiguration config)
        {
            string logPath = config.GetValue<string>("logpath", Environment.CurrentDirectory);
            string logName = config.GetValue<string>("logfilename", "log.log");
            ILogger logger = new FileLogger();
            logger.Init(logPath, logName);

            return logger;
        }
    }
}
