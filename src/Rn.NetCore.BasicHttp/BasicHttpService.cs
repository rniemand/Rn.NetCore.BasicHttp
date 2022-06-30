using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Rn.NetCore.BasicHttp;

public interface IBasicHttpService
{
  Task<HttpResponseMessage> GetUrlAsync(string url);

  Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, int timeout, CancellationToken cancellationToken);
  Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken cancellationToken);

  Task<byte[]> GetByteArrayAsync(string requestUri);
}

public class BasicHttpService : IBasicHttpService
{
  private readonly IHttpClient _httpClient;

  public BasicHttpService(IHttpClientFactory httpClientFactory)
  {
    var handler = new TimeoutHandler
    {
      DefaultTimeout = TimeSpan.FromSeconds(5),
      InnerHandler = new HttpClientHandler()
    };

    _httpClient = httpClientFactory.GetHttpClient(handler);
    _httpClient.Timeout = Timeout.InfiniteTimeSpan;
  }


  // Public methods
  public async Task<HttpResponseMessage> GetUrlAsync(string url) =>
    await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));

  public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, int timeout, CancellationToken cancellationToken)
  {
    if (timeout > 0)
      message.SetTimeout(TimeSpan.FromMilliseconds(timeout));

    return await _httpClient.SendAsync(message, cancellationToken);
  }

  public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage message, CancellationToken cancellationToken)
    => await SendAsync(message, 0, cancellationToken);

  public async Task<byte[]> GetByteArrayAsync(string requestUri)
    => await _httpClient.GetByteArrayAsync(requestUri);
}
