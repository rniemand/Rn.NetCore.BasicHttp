using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Rn.NetCore.BasicHttpService.Extensions;

namespace Rn.NetCore.BasicHttpService.Handlers
{
  // https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
  // https://thomaslevesque.com/2018/02/25/better-timeout-handling-with-httpclient/

  public class TimeoutHandler : DelegatingHandler
  {
    public TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromSeconds(100);

    // Constructors
    public TimeoutHandler()
    { }

    public TimeoutHandler(HttpMessageHandler innerHandler)
      : base(innerHandler)
    { }


    // TimeoutHandler methods
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      using var cts = GetCancellationTokenSource(request, cancellationToken);

      try
      {
        return await base.SendAsync(request, cts?.Token ?? cancellationToken);
      }
      catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
      {
        throw new TimeoutException();
      }
    }

    private CancellationTokenSource GetCancellationTokenSource(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      var timeout = request.GetTimeout() ?? DefaultTimeout;

      if (timeout == Timeout.InfiniteTimeSpan)
      {
        // No need to create a CTS if there's no timeout
        return null;
      }

      var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
      cts.CancelAfter(timeout);
      return cts;
    }
  }
}
