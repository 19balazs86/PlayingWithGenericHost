using Serilog;

namespace PlayingWithGenericHost.Service
{
    public class PeriodicTimerService : BackgroundService
    {
        // This is a better way for timing than the old System.Threading.Timer
        // Nick Chapsas: https://www.youtube.com/watch?v=J4JL4zR_l-0

        private readonly PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromMilliseconds(1_000));

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                await doWork();
            }
        }

        private static async Task doWork()
        {
            Log.Information("PeriodicTimer: {now}", DateTime.Now.ToString("HH:mm:ss:ff"));

            await Task.Delay(500);
        }
    }
}
