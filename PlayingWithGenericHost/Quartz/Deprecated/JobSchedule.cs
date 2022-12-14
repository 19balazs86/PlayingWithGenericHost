namespace PlayingWithGenericHost.Quartz.Deprecated
{
    public interface IJobSchedule
    {
        Type JobType { get; }
        string CronExpression { get; }
    }

    public class JobSchedule<T> : IJobSchedule
    {
        public Type JobType => typeof(T);
        public string CronExpression { get; private set; }

        public JobSchedule(string cronExpression) => CronExpression = cronExpression;
    }
}
