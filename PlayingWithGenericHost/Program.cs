using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
    // To make it async: *.csproj add this line <LangVersion>latest</LangVersion>
    public static async Task Main(string[] args)
    {
      IHostBuilder hostBuilder = new HostBuilder()
        .ConfigureHostConfiguration(configureHostConfiguration)
        .ConfigureAppConfiguration(configureAppConfiguration)
        .ConfigureLogging(configureLogging)
        .ConfigureServices(configureServices)
        .UsePrinterService() // Add: HostedService
        .UseSerilog(configureSerilog);

      bool isService = !(Debugger.IsAttached || args.Contains("--console"));

      if (isService)
        await hostBuilder.RunAsServiceAsync();
      else
        await hostBuilder.RunConsoleAsync(); // Enables console support, waits for Ctrl+C ...

      ////Start and wait for shutdown.
      //IHost host = hostBuilder.Build();

      //using (host)
      //{
      //  await host.StartAsync();

      //  await host.WaitForShutdownAsync();
      //}
    }

    private static void configureServices(HostBuilderContext hostContext, IServiceCollection services)
    {
      IConfiguration configuration = hostContext.Configuration;

      //services.AddOptions();
      //services.Configure<FileWriterConfig>(configuration.GetSection("FileWriter"));

      // --> Prepare configurations.
      FileWriterConfig fwConfig = new FileWriterConfig();
      configuration.GetSection("FileWriter").Bind(fwConfig);

      services.AddSingleton(fwConfig);

      // --> Add: HostedService.
      // !! Stopping in reverse order of adding.
      services.AddHostedService<FileWriterService>();
      services.AddHostedService<QuartzHostedService>();
      //services.AddHostedService<PrinterService>(); // UsePrinterService()

      // --> Install-Package Microsoft.Extensions.Http
      //services.AddHttpClient(...);

      // --> Add Quartz services
      services.AddSingleton<IJobFactory, QuartzJobFactory>();
      services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

      // --> Add our job
      services.AddSingleton<QuartzJobRunner>();
      services.AddSingleton<HelloWorldJob>();
      services.AddSingleton<IJobSchedule>(new JobSchedule<HelloWorldJob>("0/5 * * * * ?")); // Run every 5 seconds
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
