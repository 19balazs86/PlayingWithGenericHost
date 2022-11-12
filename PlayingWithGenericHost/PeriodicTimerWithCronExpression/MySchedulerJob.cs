using Serilog;

namespace PlayingWithGenericHost.PeriodicTimerWithCronExpression
{
    public class MySchedulerJob : CronBackgroundJobBase
    {
        public MySchedulerJob(CronSettings<MySchedulerJob> settings) : base(settings.CronExpression, settings.TimeZone)
        {

        }

        protected override async Task DoWork(CancellationToken stoppingToken)
        {
            Log.Information("Cron-MySchedulerJob Running... at {now}", DateTime.Now.ToString("HH:mm:ss:ff"));

            await Task.Delay(500);
        }
    }
}
