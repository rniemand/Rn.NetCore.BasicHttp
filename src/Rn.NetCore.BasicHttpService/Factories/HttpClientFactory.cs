using System.Net;
using System.Net.Http;
using Rn.NetCore.BasicHttpService.Handlers;
using Rn.NetCore.BasicHttpService.Wrappers;

namespace Rn.NetCore.BasicHttpService.Factories
{
  public interface IHttpClientFactory
  {
    IWebProxy DefaultProxy { get; set; }

    IHttpClient GetHttpClient();
    IHttpClient GetHttpClient(HttpMessageHandler handler);
    IHttpClient GetHttpClient(HttpMessageHandler handler, bool disposeHandler);
    IHttpClient GetHttpClient(HttpClientHandler clientHandler);
    IHttpClient GetHttpClient(DelegatingHandler delegatingHandler);
    IHttpClient GetHttpClient(TimeoutHandler timeoutHandler);
  }

  public class HttpClientFactory : IHttpClientFactory
  {
    public IWebProxy DefaultProxy
    {
      get => HttpClient.DefaultProxy;
      set => HttpClient.DefaultProxy = value;
    }

    public IHttpClient GetHttpClient()
      => new HttpClientWrapper();

    public IHttpClient GetHttpClient(HttpMessageHandler handler)
      => new HttpClientWrapper(handler);

    public IHttpClient GetHttpClient(HttpMessageHandler handler, bool disposeHandler)
      => new HttpClientWrapper(handler, disposeHandler);

    public IHttpClient GetHttpClient(HttpClientHandler clientHandler)
      => new HttpClientWrapper(clientHandler);

    public IHttpClient GetHttpClient(DelegatingHandler delegatingHandler)
      => new HttpClientWrapper(delegatingHandler);

    public IHttpClient GetHttpClient(TimeoutHandler timeoutHandler)
      => new HttpClientWrapper(timeoutHandler);
  }
}
