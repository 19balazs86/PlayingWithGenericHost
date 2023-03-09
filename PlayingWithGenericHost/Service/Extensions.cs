namespace PlayingWithGenericHost.Service;

public static class Extensions
{
    public static IHostBuilder UsePrinterService(this IHostBuilder hostBuilder)
        => hostBuilder.ConfigureServices(services => services.AddHostedService<PrinterService>());
}
