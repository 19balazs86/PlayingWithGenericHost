using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace PlayingWithGenericHost.Service
{
  public class PrinterService : BackgroundService
  {
    public override Task StartAsync(CancellationToken cancellationToken)
    {
      Log.Information("Printer is started.");

      return base.StartAsync(cancellationToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
      Log.Information("Printer is stopped.");

      return base.StopAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        Log.Information($"Printer is working.");

        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
      }
    }
  }
}
