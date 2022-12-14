using Quartz;

namespace PlayingWithGenericHost.Quartz
{
    [DisallowConcurrentExecution]
    public class HelloWorldJob : IJob
    {
        private readonly ILogger<HelloWorldJob> _logger;

        public HelloWorldJob(ILogger<HelloWorldJob> logger) => _logger = logger;

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world job!");

            return Task.CompletedTask;
        }
    }
}
