using NSubstitute;
using Rn.NetCore.BasicHttp.Factories;
using Rn.NetCore.Common.Logging;

namespace Rn.NetCore.BasicHttp.T1.Tests.BasicHttpServiceTests
{
  public static class TestHelper
  {
    public static BasicHttpService GetService(
      ILoggerAdapter<BasicHttpService> logger = null,
      IHttpClientFactory httpClientFactory = null)
    {
      // TODO: [TESTS] (TestHelper.GetService) Add tests
      return new BasicHttpService(
        logger ?? Substitute.For<ILoggerAdapter<BasicHttpService>>(),
        httpClientFactory ?? Substitute.For<IHttpClientFactory>()
      );
    }
  }
}
