using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Rn.NetCore.Common.Logging;

namespace DevConsole
{
  internal class Program
  {
    private static IServiceProvider _services;
    private static ILoggerAdapter<Program> _logger;

    static void Main(string[] args)
    {
      ConfigureDI();

      _logger.LogInformation("Hello World");
      Console.WriteLine("Hello World!");
    }


    // DI Related methods
    private static void ConfigureDI()
    {
      var services = new ServiceCollection();

      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .Build();

      services
        // Configuration
        .AddSingleton<IConfiguration>(config)

        // Logging
        .AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>))
        .AddLogging(loggingBuilder =>
        {
          // configure Logging with NLog
          loggingBuilder.ClearProviders();
          loggingBuilder.SetMinimumLevel(LogLevel.Trace);
          loggingBuilder.AddNLog(config);
        });

      _services = services.BuildServiceProvider();
      _logger = _services.GetService<ILoggerAdapter<Program>>();
    }
  }
}
