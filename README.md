# Playing with GenericHost
This console application is an example of utilizing the `GenericHost` as a known `WorkerService`.

[Separate branch](https://github.com/19balazs86/PlayingWithGenericHost/tree/netcoreapp2.2) with the .NET Core 2.2 version.

#### What is the HostBulder?
- `HostBuilder` is a lightweight version of the `WebHostBuilder` and it does not process HTTP requests.
- `HostBuilder` similarly like the `WebHostBuilder`, allows to configure services, using [dependency injection](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/dependency-injection), [HttpClientFactory](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/http-requests), [logging](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/logging) + [Serilog](https://github.com/serilog/serilog-extensions-hosting) and so on.

#### Run this application as a Windows Service
1. Publish the application with the predefined PublishAsWinService.pubxml (c:\svc)
2. Run the WinServiceCommands.bat file (create, start, stop, delete functions)

#### Examples
- PrinterService: a simple example of using `BackgroundService` as a base class.
- FileWriterService: a simple example of using `IHostedService` and a Timer in it.
- `Quartz`: using the [Quartz-scheduler / Quartz.NET](https://www.quartz-scheduler.net/) to create background process timing with cron expression.
- Use the `System.Threading.Channels` library to create a bounded channel/in-memory queue by utilizing IAsyncEnumerable.
- Example of [scheduling repeating tasks](https://youtu.be/J4JL4zR_l-0) 📽️*12min-NickChapsas* using [PeriodicTimer](https://learn.microsoft.com/en-us/dotnet/api/system.threading.periodictimer).
- [Azure Application Insights for WorkerService](https://github.com/19balazs86/AzureAppInsights) 👤*My repository*.
- [Hangfire](https://www.hangfire.io) example 👤[My repository](https://github.com/19balazs86/PlayingWithHangfire).

#### Resources
- 📚 Microsoft:
  - [.NET Generic Host](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/generic-host)
  - [Background tasks with hosted services](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/hosted-services)
  - [Host ASP.NET Core in a Windows Service](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service)
- [Schedule Jobs with Quartz.NET](https://code-maze.com/schedule-jobs-with-quartz-net) 📓*Code-Maze*
- 📓 *Andrew Lock*
  - [Creating a Quartz.NET hosted service](https://andrewlock.net/creating-a-quartz-net-hosted-service-with-asp-net-core)
  - [Using scoped services inside a Quartz.NET hosted service](https://andrewlock.net/using-scoped-services-inside-a-quartz-net-hosted-service-with-asp-net-core)
  - [Introducing IHostLifetime and untangling the Generic Host startup interactions](https://andrewlock.net/introducing-ihostlifetime-and-untangling-the-generic-host-startup-interactions)
- [System.Threading.Channels](https://docs.microsoft.com/en-us/dotnet/api/system.threading.channels)
  - [Introduction to System.Threading.Channels](https://www.stevejgordon.co.uk/an-introduction-to-system-threading-channels) 📓*Steve Gordon*
  - [Exploring System.Threading.Channels](https://ndportmann.com/system-threading-channels/) 📓*Nicolas Portmann*
  - [Producer-Consumer application with Channels](https://code-maze.com/dotnet-producer-consumer-channels/) 📓*Code-Maze*
- [Coravel](https://docs.coravel.net) *(Task Scheduling, Caching, Queuing, Event Broadcasting)*
  - [Scheduled job with Worker Service](https://dev.to/jamesmh/building-a-net-core-scheduled-job-worker-service-376h) 📓*James Hickey*

