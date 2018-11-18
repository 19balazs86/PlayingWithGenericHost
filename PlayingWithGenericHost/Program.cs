using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlayingWithGenericHost.Service;
using Serilog;

namespace PlayingWithGenericHost
{
  public class Program
  {
    // To make it async: *.csproj add this line <LangVersion>latest</LangVersion>
    public static async Task Main(string[] args)
    {
      IHostBuilder hostBuilder = new HostBuilder()
        .ConfigureAppConfiguration((hostContext, config) =>
        {
          // Init: Configuration
          config.AddJsonFile("appsettings.json", optional: true);
          config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
          config.AddEnvironmentVariables();
          config.AddCommandLine(args.Where(arg => arg != "--console").ToArray());

          // --> Create: Serilog
          Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config.Build())
            .CreateLogger();
        })
        .ConfigureServices((hostContext, services) =>
        {
          //services.AddOptions();
          //services.Configure<FileWriterConfig>(hostContext.Configuration.GetSection("FileWriter"));

          FileWriterConfig fwConfig = new FileWriterConfig();
          hostContext.Configuration.GetSection("FileWriter").Bind(fwConfig);

          services.AddSingleton(fwConfig);

          // --> Add: HostedService
          services.AddHostedService<FileWriterService>();
        })
        //.ConfigureLogging((hostingContext, logging) =>
        //{
        //  logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        //  logging.AddConsole();
        //  logging.AddDebug();
        //})
        .UseSerilog();

      bool isService = !(Debugger.IsAttached || args.Contains("--console"));

      if (isService)
        await hostBuilder.RunAsServiceAsync();
      else
        await hostBuilder.RunConsoleAsync();

      //await host.RunAsync();
      //return host.RunAsync();
    }
  }
}
