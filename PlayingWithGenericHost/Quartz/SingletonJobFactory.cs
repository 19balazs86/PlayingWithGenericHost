using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace PlayingWithGenericHost.Quartz
{
  public class SingletonJobFactory : IJobFactory
  {
    private readonly IServiceProvider _serviceProvider;
    public SingletonJobFactory(IServiceProvider serviceProvider)
      => _serviceProvider = serviceProvider;

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
      => _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;

    public void ReturnJob(IJob job) { }
  }
}
