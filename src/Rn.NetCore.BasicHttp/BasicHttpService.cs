using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.BasicHttp;

public interface IBasicHttpService
{
  Task<HttpResponseMessage> GetUrlAsync(string url);

  Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, int timeoutMs = 10000, CancellationToken cancellationToken = default);
  Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken);

  Task<byte[]> GetByteArrayAsync(string requestUri);
}

public class BasicHttpService : IBasicHttpService
{
  private readonly ILoggerAdapter<BasicHttpService> _logger;
  private readonly IHttpClient _httpClient;

  // Constructor
  public BasicHttpService(
    ILoggerAdapter<BasicHttpService> logger,
    IHttpClientFactory httpClientFactory)
  {
    // TODO: [TESTS] (BasicHttpService) Add tests
    _logger = logger;

    var handler = new TimeoutHandler
    {
      DefaultTimeout = TimeSpan.FromSeconds(5),
      InnerHandler = new HttpClientHandler()
    };

    _httpClient = httpClientFactory.GetHttpClient(handler);
    _httpClient.Timeout = Timeout.InfiniteTimeSpan;
    _logger.LogDebug("New instance created");
  }


  // Interface methods
  public async Task<HttpResponseMessage> GetUrlAsync(string url)
  {
    var request = new HttpRequestMessage(HttpMethod.Get, url);
    return await _httpClient.SendAsync(request);
  }

  public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage,
    int timeoutMs = 0,
    CancellationToken cancellationToken = default)
  {
    if (timeoutMs > 0)
      requestMessage.SetTimeout(TimeSpan.FromMilliseconds(timeoutMs));

    return await _httpClient.SendAsync(requestMessage, cancellationToken);
  }

  public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage requestMessage, CancellationToken cancellationToken)
    => await SendAsync(requestMessage, 0, cancellationToken);

  public async Task<byte[]> GetByteArrayAsync(string requestUri)
    => await _httpClient.GetByteArrayAsync(requestUri);
}
