using Quartz;

namespace PlayingWithGenericHost.PeriodicTimerWithCronExpression
{
    public abstract class CronBackgroundJobBase : BackgroundService
    {
        private PeriodicTimer? _timer;

        private readonly CronExpression _cronExpression;

        public CronBackgroundJobBase(string rawCronExpression, TimeZoneInfo timeZone)
        {
            if (!CronExpression.IsValidExpression(rawCronExpression))
                throw new InvalidOperationException($"Invalid CronExpression: '{rawCronExpression}'.");

            _cronExpression = new CronExpression(rawCronExpression) { TimeZone = timeZone };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DateTimeOffset? nextOcurrence = _cronExpression.GetNextValidTimeAfter(DateTime.UtcNow);

            if (nextOcurrence.HasValue)
            {

                TimeSpan delay = nextOcurrence.Value - DateTimeOffset.UtcNow;

                _timer = new PeriodicTimer(delay);

                if (await _timer.WaitForNextTickAsync(stoppingToken))
                {
                    _timer.Dispose();
                    _timer = null;

                    await DoWork(stoppingToken);

                    // Reschedule
                    await ExecuteAsync(stoppingToken);
                }
            }
        }

        protected abstract Task DoWork(CancellationToken stoppingToken);
    }
}
