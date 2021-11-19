﻿using System.Threading;
using NSubstitute;
using NUnit.Framework;
using Rn.NetCore.BasicHttpService.Factories;
using Rn.NetCore.BasicHttpService.Handlers;
using Rn.NetCore.BasicHttpService.Wrappers;

namespace Rn.NetCore.BasicHttpService.T1.Tests.BasicHttpServiceTests
{
  [TestFixture]
  public class ConstructorTests
  {
    [Test]
    public void BasicHttpService_GivenConstructed_ShouldCallGetHttpClient()
    {
      // arrange
      var httpClientFactory = Substitute.For<IHttpClientFactory>();

      // act
      TestHelper.GetService(
        httpClientFactory: httpClientFactory
      );

      // assert
      httpClientFactory.Received(1).GetHttpClient(Arg.Any<TimeoutHandler>());
    }

    [Test]
    public void BasicHttpService_GivenConstructed_ShouldDefaultTimeout()
    {
      // arrange
      var httpClientFactory = Substitute.For<IHttpClientFactory>();
      var httpClient = Substitute.For<IHttpClient>();

      httpClientFactory
        .GetHttpClient(Arg.Any<TimeoutHandler>())
        .Returns(httpClient);

      // act
      TestHelper.GetService(
        httpClientFactory: httpClientFactory
      );

      // assert
      httpClient.Received(1).Timeout = Timeout.InfiniteTimeSpan;
    }
  }
}
