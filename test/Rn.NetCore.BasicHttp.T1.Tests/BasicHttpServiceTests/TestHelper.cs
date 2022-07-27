using NSubstitute;

namespace Rn.NetCore.BasicHttp.T1.Tests.BasicHttpServiceTests;

public static class TestHelper
{
  public static BasicHttpService GetService(IHttpClientFactory? httpClientFactory = null) =>
    new(httpClientFactory ?? Substitute.For<IHttpClientFactory>());
}
