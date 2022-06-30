using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.BasicHttp;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddRnBasicHttp(this IServiceCollection services, IConfiguration configuration)
  {
    // Logging
    services.TryAddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

    return services
      .AddSingleton<IHttpClientFactory, HttpClientFactory>()
      .AddSingleton<IBasicHttpService, BasicHttpService>();
  }
}
