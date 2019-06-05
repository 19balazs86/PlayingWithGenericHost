using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace PlayingWithGenericHost.Quartz
{
  public class QuartzJobFactory : IJobFactory
  {
    private readonly IServiceProvider _serviceProvider;
    public QuartzJobFactory(IServiceProvider serviceProvider)
      => _serviceProvider = serviceProvider;

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
      => _serviceProvider.GetRequiredService<QuartzJobRunner>();

    public void ReturnJob(IJob job) { }
  }
}
