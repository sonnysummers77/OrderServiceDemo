using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace OrderServiceDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) => 
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.Sources.Clear();
                    config
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.ClearProviders();

                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .CreateLogger();

                    builder.AddSerilog(Log.Logger);

                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        builder.AddDebug();
                    }
                })
                .UseUrls("http://*:5000")
                .UseStartup<Startup>()
                .Build();
    }
}
