using System;
using System.Net.Http;

namespace Rn.NetCore.BasicHttp;

public static class RequestMessageExtensions
{
  private const string TimeoutPropertyKey = "RequestTimeout";

  public static TimeSpan? GetTimeout(this HttpRequestMessage request)
  {
    if (request == null)
      throw new ArgumentNullException(nameof(request));

    // TODO: [RequestMessageExtensions.GetTimeout] (REPLACE) Make use of options
    if (request.Properties.TryGetValue(TimeoutPropertyKey, out var value) && value is TimeSpan timeout)
      return timeout;

    return null;
  }

  public static void SetTimeout(this HttpRequestMessage request, TimeSpan? timeout)
  {
    if (request == null)
      throw new ArgumentNullException(nameof(request));

    request.Properties[TimeoutPropertyKey] = timeout;
  }
}
