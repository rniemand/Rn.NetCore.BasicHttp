using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Rn.NetCore.BasicHttp.Extensions;
using Rn.NetCore.BasicHttp.Factories;
using Rn.NetCore.BasicHttp.Handlers;
using Rn.NetCore.BasicHttp.Wrappers;

namespace Rn.NetCore.BasicHttp.T1.Tests.BasicHttpServiceTests;

[TestFixture]
public class SendAsyncTests
{
  private const string Url = "http://google.com/";

  [Test]
  public async Task SendAsync_GivenCalled_ShouldCallSendAsync()
  {
    // arrange
    var httpClientFactory = Substitute.For<IHttpClientFactory>();
    var httpClient = Substitute.For<IHttpClient>();
    var requestMessage = new HttpRequestMessage(HttpMethod.Get, Url);

    httpClientFactory
      .GetHttpClient(Arg.Any<TimeoutHandler>())
      .Returns(httpClient);

    var httpService = TestHelper.GetService(
      httpClientFactory: httpClientFactory
    );

    // act
    await httpService.SendAsync(requestMessage);

    // assert
    await httpClient.Received(1).SendAsync(requestMessage, CancellationToken.None);
  }

  [Test]
  public async Task SendAsync_GivenCancellationToken_ShouldUseCancellationToken()
  {
    // arrange
    var httpClientFactory = Substitute.For<IHttpClientFactory>();
    var httpClient = Substitute.For<IHttpClient>();
    var requestMessage = new HttpRequestMessage(HttpMethod.Get, Url);
    var cancellationToken = new CancellationTokenSource().Token;

    httpClientFactory
      .GetHttpClient(Arg.Any<TimeoutHandler>())
      .Returns(httpClient);

    var httpService = TestHelper.GetService(
      httpClientFactory: httpClientFactory
    );

    // act
    await httpService.SendAsync(requestMessage, cancellationToken);

    // assert
    await httpClient.Received(1).SendAsync(requestMessage, cancellationToken);
  }

  [Test]
  public async Task SendAsync_GivenCalledWithTimeout_ShouldAppendTimeout()
  {
    // arrange
    var httpClientFactory = Substitute.For<IHttpClientFactory>();
    var httpClient = Substitute.For<IHttpClient>();
    var requestMessage = new HttpRequestMessage(HttpMethod.Get, Url);

    httpClientFactory
      .GetHttpClient(Arg.Any<TimeoutHandler>())
      .Returns(httpClient);

    var httpService = TestHelper.GetService(
      httpClientFactory: httpClientFactory
    );

    // act
    await httpService.SendAsync(requestMessage, 120);

    // assert
    await httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(m =>
      m.GetTimeout() == TimeSpan.FromMilliseconds(120)
    ), CancellationToken.None);
  }

  [Test]
  public async Task SendAsync_GivenCalled_ShouldReturnResponse()
  {
    // arrange
    var httpClientFactory = Substitute.For<IHttpClientFactory>();
    var httpClient = Substitute.For<IHttpClient>();
    var requestMessage = new HttpRequestMessage(HttpMethod.Get, Url);
    var responseMessage = new HttpResponseMessage();

    httpClientFactory
      .GetHttpClient(Arg.Any<TimeoutHandler>())
      .Returns(httpClient);

    httpClient
      .SendAsync(requestMessage, CancellationToken.None)
      .Returns(responseMessage);

    var httpService = TestHelper.GetService(
      httpClientFactory: httpClientFactory
    );

    // act
    var response = await httpService.SendAsync(requestMessage);

    // assert
    Assert.IsNotNull(response);
    Assert.IsInstanceOf<HttpResponseMessage>(response);
    Assert.AreEqual(responseMessage, response);
  }
}
