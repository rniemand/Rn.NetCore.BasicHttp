using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace Rn.NetCore.BasicHttp.T1.Tests.BasicHttpServiceTests;

[TestFixture]
public class GetByteArrayAsyncTests
{
  private const string Url = "http://google.com/";

  [Test]
  public async Task GetByteArrayAsync_GivenCalled_ShouldCallGetByteArrayAsync()
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
    await httpService.GetByteArrayAsync(Url);

    // assert
    await httpClient.Received(1).GetByteArrayAsync(Url);
  }

  [Test]
  public async Task GetByteArrayAsync_GivenCalled_ShouldReturnResponse()
  {
    // arrange
    var httpClientFactory = Substitute.For<IHttpClientFactory>();
    var httpClient = Substitute.For<IHttpClient>();
    var responseBytes = Array.Empty<byte>();


    httpClientFactory
      .GetHttpClient(Arg.Any<TimeoutHandler>())
      .Returns(httpClient);

    httpClient.GetByteArrayAsync(Url).Returns(responseBytes);

    var httpService = TestHelper.GetService(
      httpClientFactory: httpClientFactory
    );

    // act
    var response = await httpService.GetByteArrayAsync(Url);

    // assert
    Assert.IsNotNull(response);
    Assert.IsInstanceOf<byte[]>(response);
    Assert.AreEqual(responseBytes, response);
  }
}
