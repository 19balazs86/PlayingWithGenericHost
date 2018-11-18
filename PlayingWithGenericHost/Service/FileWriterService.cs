using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PlayingWithGenericHost.Service
{
  public class FileWriterService : IHostedService, IDisposable
  {
    private readonly FileWriterConfig _config;
    private readonly ILogger<FileWriterService> _logger;
    private Timer _timer;

    public FileWriterService(FileWriterConfig config, ILogger<FileWriterService> logger)
    {
      _config = config;
      _logger = logger;
      // This is not necessary for Serilog. Just a demonstration, that the UseSerilog extension method is working.
      // You can use Log.Information without ILogger.
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      _logger.LogInformation("Starting");

      _timer = new Timer(x => doWork(), null, TimeSpan.Zero, TimeSpan.FromSeconds(_config.EverySeconds));

      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      _logger.LogInformation("Stopping.");

      _timer?.Change(Timeout.Infinite, 0);

      return Task.CompletedTask;
    }

    public void Dispose()
    {
      _logger.LogInformation("Dispose.");

      _timer?.Dispose();
    }

    private void doWork()
    {
      _logger.LogInformation("DoWork.");

      string path = _config.WriteToPath;

      if (File.Exists(path))
      {
        using (StreamWriter sw = File.AppendText(path))
          sw.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
      }
      else
      {
        using (StreamWriter sw = File.CreateText(path))
          sw.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
      }
    }
  }
}
