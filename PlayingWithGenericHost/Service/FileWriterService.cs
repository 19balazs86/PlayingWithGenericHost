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
      _logger.LogInformation("FileWriterService is starting.");

      _timer = new Timer(heartbeat, null, TimeSpan.Zero, TimeSpan.FromSeconds(_config.EverySeconds));

      return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      _logger.LogInformation("FileWriterService is stopping.");

      _timer?.Change(Timeout.Infinite, 0);

      return Task.CompletedTask;
    }

    public void Dispose()
    {
      _logger.LogInformation("Dispose.");

      _timer?.Dispose();
    }

    private void heartbeat(object state)
    {
      // Or: new Timer(x => _ = doWork(), ...
      // https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md#timer-callbacks
      _ = doWork(); // Discard the result
    }

    private async Task doWork()
    {
      _logger.LogInformation("FileWriterService is working.");

      try
      {
        using (FileStream fs = File.Open(_config.WriteToPath, FileMode.Append, FileAccess.Write))
        using (StreamWriter sw = new StreamWriter(fs))
        {
          await sw.WriteLineAsync(DateTime.Now.ToString("HH:mm:ss"));
          await sw.FlushAsync();
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error during the work.");
      }
    }
  }
}
