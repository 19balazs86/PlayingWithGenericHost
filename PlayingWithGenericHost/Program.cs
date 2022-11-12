using PlayingWithGenericHost.PeriodicTimerWithCronExpression;
using PlayingWithGenericHost.Quartz;
using PlayingWithGenericHost.Service;
using PlayingWithGenericHost.ThreadingChannels;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
            // Try to drain the channel.
            services.Configure<HostOptions>(option => option.ShutdownTimeout = TimeSpan.FromSeconds(15));

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
            //services.AddHostedService<FileWriterService>();
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

            #region ThreadingChannels
            services.AddSingleton<MessageChannel>();
            services.AddHostedService<ChannelReaderService>();
            services.AddHostedService<ChannelWriterService>();
            // !!Stopping in reverse order of adding. We should stop the writer first.
            #endregion

            #region PeriodicTimerWithCronExpression

            // Simple way of using the PeriodicTimer
            services.AddHostedService<PeriodicTimerHeartbeatService>();

            services.AddCronJob<MySchedulerJob>(options =>
            {
                options.CronExpression = "*/5 * * * * ?"; // Run every 5 seconds
                options.TimeZone       = TimeZoneInfo.Local;
            });

            #endregion
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
}
