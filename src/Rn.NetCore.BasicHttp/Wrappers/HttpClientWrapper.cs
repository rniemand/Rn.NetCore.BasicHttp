using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Rn.NetCore.BasicHttp.Handlers;

namespace Rn.NetCore.BasicHttp.Wrappers
{
  public interface IHttpClient
  {
    HttpRequestHeaders DefaultRequestHeaders { get; }
    Version DefaultRequestVersion { get; set; }
    HttpVersionPolicy DefaultVersionPolicy { get; set; }
    Uri? BaseAddress { get; set; }
    TimeSpan Timeout { get; set; }
    long MaxResponseContentBufferSize { get; set; }

    Task<string> GetStringAsync(string requestUri);
    Task<string> GetStringAsync(Uri requestUri);
    Task<string> GetStringAsync(string requestUri, CancellationToken cancellationToken);
    Task<string> GetStringAsync(Uri requestUri, CancellationToken cancellationToken);
    Task<byte[]> GetByteArrayAsync(string requestUri);
    Task<byte[]> GetByteArrayAsync(Uri requestUri);
    Task<byte[]> GetByteArrayAsync(string requestUri, CancellationToken cancellationToken);
    Task<byte[]> GetByteArrayAsync(Uri requestUri, CancellationToken cancellationToken);
    Task<Stream> GetStreamAsync(string requestUri);
    Task<Stream> GetStreamAsync(string requestUri, CancellationToken cancellationToken);
    Task<Stream> GetStreamAsync(Uri requestUri);
    Task<Stream> GetStreamAsync(Uri requestUri, CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetAsync(string requestUri);
    Task<HttpResponseMessage> GetAsync(Uri requestUri);
    Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption);
    Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption);
    Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken);
    Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content);
    Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content);
    Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content);
    Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content);
    Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content);
    Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken);
    Task<HttpResponseMessage> DeleteAsync(string requestUri);
    Task<HttpResponseMessage> DeleteAsync(Uri requestUri);
    Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken);
    Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken);
    HttpResponseMessage Send(HttpRequestMessage request);
    HttpResponseMessage Send(HttpRequestMessage request, HttpCompletionOption completionOption);
    HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken);
    HttpResponseMessage Send(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken);
    void CancelPendingRequests();
    void Dispose();
  }

  public class HttpClientWrapper : IHttpClient
  {
    public HttpRequestHeaders DefaultRequestHeaders
      => _httpClient.DefaultRequestHeaders;

    public Version DefaultRequestVersion
    {
      get => _httpClient.DefaultRequestVersion;
      set => _httpClient.DefaultRequestVersion = value;
    }

    public HttpVersionPolicy DefaultVersionPolicy
    {
      get => _httpClient.DefaultVersionPolicy;
      set => _httpClient.DefaultVersionPolicy = value;
    }

    public Uri? BaseAddress
    {
      get => _httpClient.BaseAddress;
      set => _httpClient.BaseAddress = value;
    }

    public TimeSpan Timeout
    {
      get => _httpClient.Timeout;
      set => _httpClient.Timeout = value;
    }

    public long MaxResponseContentBufferSize
    {
      get => _httpClient.MaxResponseContentBufferSize;
      set => _httpClient.MaxResponseContentBufferSize = value;
    }


    // Constructors
    private readonly HttpClient _httpClient;

    public HttpClientWrapper()
    {
      _httpClient = new HttpClient();
    }

    public HttpClientWrapper(HttpMessageHandler handler)
    {
      _httpClient = new HttpClient(handler);
    }

    public HttpClientWrapper(HttpMessageHandler handler, bool disposeHandler)
    {
      _httpClient = new HttpClient(handler, disposeHandler);
    }

    public HttpClientWrapper(HttpClientHandler clientHandler)
    {
      _httpClient = new HttpClient(clientHandler);
    }

    public HttpClientWrapper(DelegatingHandler delegatingHandler)
    {
      delegatingHandler.InnerHandler ??= new HttpClientHandler();
      _httpClient = new HttpClient(delegatingHandler);

      if (delegatingHandler is TimeoutHandler)
      {
        _httpClient.Timeout = System.Threading.Timeout.InfiniteTimeSpan;
      }
    }


    // GetStringAsync(...)
    public async Task<string> GetStringAsync(string requestUri)
      => await _httpClient.GetStringAsync(requestUri);

    public async Task<string> GetStringAsync(Uri requestUri)
      => await _httpClient.GetStringAsync(requestUri);

    public async Task<string> GetStringAsync(string requestUri, CancellationToken cancellationToken)
      => await _httpClient.GetStringAsync(requestUri, cancellationToken);

    public async Task<string> GetStringAsync(Uri requestUri, CancellationToken cancellationToken)
      => await _httpClient.GetStringAsync(requestUri, cancellationToken);


    // GetByteArrayAsync(...)
    public async Task<byte[]> GetByteArrayAsync(string requestUri)
      => await _httpClient.GetByteArrayAsync(requestUri);

    public async Task<byte[]> GetByteArrayAsync(Uri requestUri)
      => await _httpClient.GetByteArrayAsync(requestUri);

    public async Task<byte[]> GetByteArrayAsync(string requestUri, CancellationToken cancellationToken)
      => await _httpClient.GetByteArrayAsync(requestUri, cancellationToken);

    public async Task<byte[]> GetByteArrayAsync(Uri requestUri, CancellationToken cancellationToken)
      => await _httpClient.GetByteArrayAsync(requestUri, cancellationToken);


    // GetStreamAsync(...)
    public async Task<Stream> GetStreamAsync(string requestUri)
      => await _httpClient.GetStreamAsync(requestUri);

    public async Task<Stream> GetStreamAsync(string requestUri, CancellationToken cancellationToken)
      => await _httpClient.GetStreamAsync(requestUri, cancellationToken);

    public async Task<Stream> GetStreamAsync(Uri requestUri)
      => await _httpClient.GetStreamAsync(requestUri);

    public async Task<Stream> GetStreamAsync(Uri requestUri, CancellationToken cancellationToken)
      => await _httpClient.GetStreamAsync(requestUri, cancellationToken);


    // GetAsync(...)
    public async Task<HttpResponseMessage> GetAsync(string requestUri)
      => await _httpClient.GetAsync(requestUri);

    public async Task<HttpResponseMessage> GetAsync(Uri requestUri)
      => await _httpClient.GetAsync(requestUri);

    public async Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
      => await _httpClient.GetAsync(requestUri, completionOption);

    public async Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
      => await _httpClient.GetAsync(requestUri, completionOption);

    public async Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
      => await _httpClient.GetAsync(requestUri, cancellationToken);

    public async Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
      => await _httpClient.GetAsync(requestUri, cancellationToken);

    public async Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
      => await _httpClient.GetAsync(requestUri, completionOption, cancellationToken);

    public async Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
      => await _httpClient.GetAsync(requestUri, completionOption, cancellationToken);


    // PostAsync(...)
    public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
      => await _httpClient.PostAsync(requestUri, content);

    public async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
      => await _httpClient.PostAsync(requestUri, content);

    public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
      => await _httpClient.PostAsync(requestUri, content, cancellationToken);

    public async Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
      => await _httpClient.PostAsync(requestUri, content, cancellationToken);


    // PutAsync(...)
    public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
      => await _httpClient.PutAsync(requestUri, content);

    public async Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
      => await _httpClient.PutAsync(requestUri, content);

    public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
      => await _httpClient.PutAsync(requestUri, content, cancellationToken);

    public async Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
      => await _httpClient.PutAsync(requestUri, content, cancellationToken);


    // PatchAsync(...)
    public async Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content)
      => await _httpClient.PatchAsync(requestUri, content);

    public async Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content)
      => await _httpClient.PatchAsync(requestUri, content);

    public async Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
      => await _httpClient.PatchAsync(requestUri, content, cancellationToken);

    public async Task<HttpResponseMessage> PatchAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
      => await _httpClient.PatchAsync(requestUri, content, cancellationToken);


    // DeleteAsync(...)
    public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
      => await _httpClient.DeleteAsync(requestUri);

    public async Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
      => await _httpClient.DeleteAsync(requestUri);

    public async Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
      => await _httpClient.DeleteAsync(requestUri, cancellationToken);

    public async Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
      => await _httpClient.DeleteAsync(requestUri, cancellationToken);


    // Send(...)
    public HttpResponseMessage Send(HttpRequestMessage request)
      => _httpClient.Send(request);

    public HttpResponseMessage Send(HttpRequestMessage request, HttpCompletionOption completionOption)
      => _httpClient.Send(request, completionOption);

    public HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
      => _httpClient.Send(request, cancellationToken);

    public HttpResponseMessage Send(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
      => _httpClient.Send(request, completionOption, cancellationToken);


    // SendAsync(...)
    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
      => await _httpClient.SendAsync(request);

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
      => await _httpClient.SendAsync(request, cancellationToken);

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption)
      => await _httpClient.SendAsync(request, completionOption);

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption, CancellationToken cancellationToken)
      => await _httpClient.SendAsync(request, completionOption, cancellationToken);


    // Misc...
    public void CancelPendingRequests() => _httpClient.CancelPendingRequests();

    public void Dispose() => _httpClient.Dispose();
  }
}
