using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace Rn.NetCore.BasicHttp.T1.Tests.BasicHttpServiceTests;

[TestFixture]
public class GetUrlAsyncTests
{
  private const string Url = "http://google.com/";

  [Test]
  public async Task GetUrlAsync_GivenCalled_ShouldCallSendAsync()
  {
    // arrange
    var httpClientFactory = Substitute.For<IHttpClientFactory>();
    var httpClient = Substitute.For<IHttpClient>();

    httpClientFactory
      .GetHttpClient(Arg.Any<TimeoutHandler>())
      .Returns(httpClient);

    var httpService = TestHelper.GetService(
      httpClientFactory: httpClientFactory
    );

    // act
    await httpService.GetUrlAsync(Url);

    // assert
    await httpClient.Received(1).SendAsync(Arg.Is<HttpRequestMessage>(m =>
      m.Method == HttpMethod.Get &&
      m.RequestUri.AbsoluteUri == Url
    ));
  }

  [Test]
  public async Task GetUrlAsync_GivenCalled_ShouldReturnResponseMessage()
  {
    // arrange
    var httpClientFactory = Substitute.For<IHttpClientFactory>();
    var httpClient = Substitute.For<IHttpClient>();
    var responseMessage = new HttpResponseMessage();

    httpClientFactory
      .GetHttpClient(Arg.Any<TimeoutHandler>())
      .Returns(httpClient);

    httpClient
      .SendAsync(Arg.Any<HttpRequestMessage>())
      .Returns(responseMessage);

    var httpService = TestHelper.GetService(
      httpClientFactory: httpClientFactory
    );

    // act
    var response = await httpService.GetUrlAsync(Url);

    // assert
    Assert.IsNotNull(response);
    Assert.IsInstanceOf<HttpResponseMessage>(response);
    Assert.AreEqual(responseMessage, response);
  }
}
