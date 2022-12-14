using Quartz;

namespace PlayingWithGenericHost.Quartz
{
    public static class ScheduleJobExtension
    {
        public static IServiceCollectionQuartzConfigurator ScheduleCronJob<TJob>(
            this IServiceCollectionQuartzConfigurator configurator,
            string cronExpression) where TJob : IJob
        {
            if (!CronExpression.IsValidExpression(cronExpression))
                throw new InvalidOperationException($"Invalid CronExpression: '{cronExpression}'.");

            configurator.ScheduleJob<TJob>(trigger => trigger
                .WithIdentity($"{typeof(TJob).FullName}.trigger")
                .WithCronSchedule(cronExpression)
                .WithDescription(cronExpression)
            );

            return configurator;
        }
    }
}
