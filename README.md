# Playing with GenericHost
This console application demonstrates the use of `GenericHost` as known `WorkerService`.

You can find a few examples of running background jobs in different ways.

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
- Example of Producer-Consumer pattern with TPL Dataflow.
- [Azure Application Insights for WorkerService](https://github.com/19balazs86/AzureAppInsights) 👤*My repository*.
- [Hangfire](https://www.hangfire.io) example 👤[My repository](https://github.com/19balazs86/PlayingWithHangfire).

#### Resources

- 📚 Microsoft:
  - [.NET Generic Host](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/generic-host)
  - [Background tasks with hosted services](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/hosted-services)
  - [Host ASP.NET Core in a Windows Service](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service)
- [Schedule Jobs with Quartz.NET](https://code-maze.com/schedule-jobs-with-quartz-net) 📓*Code-Maze*
- [Scheduling background tasks with Quartz](https://youtu.be/iD3jrj3RBuc) 📽️*11min-Milan (nice solution by using IConfigureOptions)*
- [Too many timers in .NET](https://www.meziantou.net/too-many-timers-in-dotnet.htm) 📓*Meziantou - Gérald Barré*
- 📓 *Andrew Lock*
  - [Creating a Quartz.NET hosted service](https://andrewlock.net/creating-a-quartz-net-hosted-service-with-asp-net-core)
  - [Using scoped services inside a Quartz.NET hosted service](https://andrewlock.net/using-scoped-services-inside-a-quartz-net-hosted-service-with-asp-net-core)
  - [Introducing IHostLifetime and untangling the Generic Host startup interactions](https://andrewlock.net/introducing-ihostlifetime-and-untangling-the-generic-host-startup-interactions)
- System Threading Channels
  - [System.Threading.Channels Namespace](https://learn.microsoft.com/en-us/dotnet/api/system.threading.channels) 📚*Microsoft-Learn*
  - [Introduction to System.Threading.Channels](https://www.stevejgordon.co.uk/an-introduction-to-system-threading-channels) 📓*Steve Gordon*
  - [Exploring System.Threading.Channels](https://ndportmann.com/system-threading-channels/) 📓*Nicolas Portmann*
  - [Producer-Consumer application with Channels](https://code-maze.com/dotnet-producer-consumer-channels/) 📓*Code-Maze*
- TPL Dataflow
  - [Implementing the producer-consumer pattern with TPL Dataflow](https://markheath.net/post/producer-consumer-pattern-tpl) 📓*Mark Heath*
  - [Task Parallel Library (TPL)](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/task-parallel-library-tpl) 📚*Microsoft-Learn*
  - [Implement a producer-consumer dataflow pattern](https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/how-to-implement-a-producer-consumer-dataflow-pattern) 📚*Microsoft-Learn*
- [Coravel](https://docs.coravel.net) *(Task Scheduling, Caching, Queuing, Event Broadcasting)*
  - [Scheduled job with Worker Service](https://dev.to/jamesmh/building-a-net-core-scheduled-job-worker-service-376h) 📓*James Hickey*

