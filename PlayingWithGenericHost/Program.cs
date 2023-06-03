using PlayingWithGenericHost.DataflowTPL;
using PlayingWithGenericHost.PeriodicTimerWithCronExpression;
using PlayingWithGenericHost.Quartz;
using PlayingWithGenericHost.Service;
using PlayingWithGenericHost.ThreadingChannels;
using Quartz;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PlayingWithGenericHost;

public static class Program
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

        if (isService && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            hostBuilder.UseWindowsService();

        try
        {
            hostBuilder.Build().Run();
        }
        catch (Exception ex)
        {
            // Can be TaskCanceledException, due to the HostOptions.ShutdownTimeout.
            Console.WriteLine($"Program.Main error: '{ex.Message}'");
        }
    }

    private static void configureServices(HostBuilderContext hostContext, IServiceCollection services)
    {
        IConfiguration configuration = hostContext.Configuration;

        // Try to drain the channel.
        services.Configure<HostOptions>(option => option.ShutdownTimeout = TimeSpan.FromSeconds(15));

        // --> Install-Package Microsoft.Extensions.Http
        //services.AddHttpClient(...);

        // You can try the services 1 by 1

        //services.AddHostedService<PrinterService>(); // UsePrinterService()

        //services.addFileWriterService(configuration);

        services.addQuartzServices();

        services.addChannelServices();

        services.addPeriodicTimerServices();

        services.addDataflowServices();
    }

    private static void addQuartzServices(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            configure.UseMicrosoftDependencyInjectionJobFactory();

            configure.ScheduleCronJob<HelloWorldJob>("*/5 * * * * ?"); // Run every 5 seconds
        });

        services.AddQuartzHostedService(configure => configure.WaitForJobsToComplete = true);

        services.AddSingleton<HelloWorldJob>();
        
        // Production, consider using a different JobStores than the default RAMJobStore
        // https://www.quartz-scheduler.net/documentation/quartz-3.x/quick-start.html
    }

    private static void addChannelServices(this IServiceCollection services)
    {
        services.AddSingleton<MessageChannel>();
        services.AddHostedService<ChannelReaderService>();
        services.AddHostedService<ChannelWriterService>();
        // !!Stopping in reverse order of adding. We should stop the writer first.
    }

    private static void addPeriodicTimerServices(this IServiceCollection services)
    {
        // Simple way of using the PeriodicTimer
        services.AddHostedService<PeriodicTimerHeartbeatService>();

        services.AddCronJob<MySchedulerJob>(options =>
        {
            options.CronExpression = "*/5 * * * * ?"; // Run every 5 seconds
            options.TimeZone       = TimeZoneInfo.Local;
        });
    }

    private static void addDataflowServices(this IServiceCollection services)
    {
        services.AddSingleton<BufferBlockSource>();
        services.AddHostedService<BufferBlockConsumerService>();
        services.AddHostedService<BufferBlockProducerService>();
    }

    private static void addFileWriterService(this IServiceCollection services, IConfiguration configuration)
    {
        //services.Configure<FileWriterConfig>(configuration.GetSection("FileWriter"));

        var fwConfig = new FileWriterConfig();
        configuration.GetSection("FileWriter").Bind(fwConfig);

        services.AddSingleton(fwConfig);

        services.AddHostedService<FileWriterService>();
    }

    private static void configureAppConfiguration(HostBuilderContext hostContext, IConfigurationBuilder configBuilder)
    {
        // CreateDefaultBuilder method do the configuration.

        //configBuilder.AddJsonFile("appsettings.json", optional: true);
        //configBuilder.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
        //configBuilder.AddEnvironmentVariables();
        //configBuilder.AddCommandLine(args.Where(arg => arg != "--console").ToArray());
    }

    private static void configureHostConfiguration(IConfigurationBuilder configBuilder)
    {
        // CreateDefaultBuilder method do the configuration.

        //if (args != null)
        //  configBuilder.AddCommandLine(args);
    }

    private static void configureLogging(HostBuilderContext hostContext, ILoggingBuilder logging)
    {
        // CreateDefaultBuilder method do the configuration.

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
