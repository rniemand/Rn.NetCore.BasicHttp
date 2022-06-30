using System;
using Microsoft.Extensions.DependencyInjection;
using Rn.NetCore.Common.Logging;

namespace DevConsole;

internal class Program
{
  static void Main(string[] args)
  {
    var logger = DIContainer.Services.GetRequiredService<ILoggerAdapter<Program>>();

    logger.LogInformation("Hello world");

    Console.WriteLine("Hello World!");
  }
}
