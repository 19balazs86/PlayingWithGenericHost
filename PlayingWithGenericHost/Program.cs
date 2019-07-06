using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlayingWithGenericHost.Quartz;
using PlayingWithGenericHost.Service;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Serilog;
using Serilog.Events;

namespace PlayingWithGenericHost
{
  public class Program
  {
    public static void Main(string[] args)
    {
      IHostBuilder hostBuilder = Host
        .CreateDefaultBuilder(/*args*/)
        .ConfigureHostConfiguration(configureHostConfiguration)
        .ConfigureAppConfiguration(configureAppConfiguration)
        .ConfigureLogging(configureLogging)
        .ConfigureServices(configureServices)
        .UsePrinterService() // Add: HostedService
        .UseSerilog(configureSerilog);

      bool isService = !(Debugger.IsAttached || args.Contains("--console"));

      if (isService) hostBuilder.UseWindowsService();

      hostBuilder.Build().Run();
    }

    private static void configureServices(HostBuilderContext hostContext, IServiceCollection services)
    {
      #region Configuration
      IConfiguration configuration = hostContext.Configuration;

      //services.AddOptions();
      //services.Configure<FileWriterConfig>(configuration.GetSection("FileWriter"));

      // --> Prepare configurations.
      FileWriterConfig fwConfig = new FileWriterConfig();
      configuration.GetSection("FileWriter").Bind(fwConfig);

      services.AddSingleton(fwConfig);
      #endregion

      // --> Add: HostedService.
      // !! Stopping in reverse order of adding.
      services.AddHostedService<FileWriterService>();
      //services.AddHostedService<PrinterService>(); // UsePrinterService()

      // --> Install-Package Microsoft.Extensions.Http
      //services.AddHttpClient(...);

      #region Quartz
      // --> Add Quartz services
      services.AddHostedService<QuartzHostedService>();

      services.AddSingleton<IJobFactory, QuartzJobFactory>();
      services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

      // --> Add Quartz jobs
      services.AddSingleton<QuartzJobRunner>();
      services.AddSingleton<HelloWorldJob>();
      services.AddSingleton<IJobSchedule>(new JobSchedule<HelloWorldJob>("*/5 * * * * ?")); // Run every 5 seconds
      #endregion
    }

    private static void configureAppConfiguration(HostBuilderContext hostContext, IConfigurationBuilder configBuilder)
    {
      configBuilder.AddJsonFile("appsettings.json", optional: true);
      configBuilder.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
      //configBuilder.AddEnvironmentVariables();
      //configBuilder.AddCommandLine(args.Where(arg => arg != "--console").ToArray());
    }

    private static void configureHostConfiguration(IConfigurationBuilder configBuilder)
    {
      //if (args != null)
      //  configBuilder.AddCommandLine(args);
    }

    private static void configureLogging(HostBuilderContext hostContext, ILoggingBuilder logging)
    {
      //logging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
      //logging.AddConsole();
      //logging.AddDebug();

      // But: .UseSerilog()
    }

    private static void configureSerilog(HostBuilderContext hostContext, LoggerConfiguration configuration)
    {
      configuration
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Quartz", LogEventLevel.Warning)
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message}{NewLine}{Exception}");
    }
  }
}
