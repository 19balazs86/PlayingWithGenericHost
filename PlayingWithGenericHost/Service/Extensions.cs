using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PlayingWithGenericHost.Service
{
  public static class Extensions
  {
    public static IHostBuilder UsePrinterService(this IHostBuilder hostBuilder)
      => hostBuilder.ConfigureServices(services => services.AddHostedService<PrinterService>());
  }
}
