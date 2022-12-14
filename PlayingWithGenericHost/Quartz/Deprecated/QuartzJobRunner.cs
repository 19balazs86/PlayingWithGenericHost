using Quartz;
using Serilog;

namespace PlayingWithGenericHost.Quartz.Deprecated
{
    public class QuartzJobRunner : IJob
    {
        private readonly IServiceProvider _serviceProvider;

        public QuartzJobRunner(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Type jobType = context.JobDetail.JobType;

            using IServiceScope scope = _serviceProvider.CreateScope();

            try
            {
                IJob job = scope.ServiceProvider.GetRequiredService(jobType) as IJob;

                await job.Execute(context);
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, $"The job is not present in the DI container for the {jobType.Name}.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An exception occurred in the {jobType.Name}.");
            }
        }
    }
}