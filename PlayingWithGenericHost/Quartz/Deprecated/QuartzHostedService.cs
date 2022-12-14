using Quartz;
using Quartz.Spi;

namespace PlayingWithGenericHost.Quartz.Deprecated
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<IJobSchedule> _jobSchedules;

        private IScheduler _scheduler;

        public QuartzHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IEnumerable<IJobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
            _jobFactory = jobFactory;
        }

        public async Task StartAsync(CancellationToken ct)
        {
            _scheduler = await _schedulerFactory.GetScheduler(ct);

            _scheduler.JobFactory = _jobFactory;

            foreach (IJobSchedule jobSchedule in _jobSchedules)
            {
                IJobDetail jobDetail = createJobDetail(jobSchedule.JobType);
                ITrigger trigger = createTrigger(jobSchedule);

                await _scheduler.ScheduleJob(jobDetail, trigger, ct);
            }

            await _scheduler.Start(ct);
        }

        public async Task StopAsync(CancellationToken ct)
            => await _scheduler?.Shutdown(waitForJobsToComplete: true, ct);

        private static IJobDetail createJobDetail(Type jobType)
            => JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();

        private static ITrigger createTrigger(IJobSchedule jobSchedule)
            => TriggerBuilder
                .Create()
                .WithIdentity($"{jobSchedule.JobType.FullName}.trigger")
                .WithCronSchedule(jobSchedule.CronExpression)
                .WithDescription(jobSchedule.CronExpression)
                .Build();
    }
}
