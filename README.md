# Playing with GenericHost
This console application is an example of using the `GenericHost` as known `WorkerService`.

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
- `System.Threading.Channels`: leverage this library to create a bounded channel/in-memory queue. Using IAsyncEnumerable.
- Example of using [PeriodicTimer](https://learn.microsoft.com/en-us/dotnet/api/system.threading.periodictimer). [YouTube video](https://www.youtube.com/watch?v=J4JL4zR_l-0) *(Nick Chapsas)*
- Azure Application Insights for WorkerService [example in my repository](https://github.com/19balazs86/AzureAppInsights).
- [Hangfire](https://www.hangfire.io) example [in my repository](https://github.com/19balazs86/PlayingWithHangfire).

#### Resources
- Microsoft:
  - [.NET Generic Host](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/generic-host).
  - [Background tasks with hosted services in ASP.NET Core](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/hosted-services).
  - [Host ASP.NET Core in a Windows Service](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service).
- Code-Maze: [Schedule Jobs with Quartz.NET](https://code-maze.com/schedule-jobs-with-quartz-net/)
- Telerik blog: [.NET Core 3 Background Services](https://www.telerik.com/blogs/.net-core-background-services).
- Glenn Condron (1 hour): [Community Standup - Takes Workers Everywhere](https://www.youtube.com/watch?v=5AEqA035o5I&feature=youtu.be&t=1709) + Docker example.
- Blog posts from *Andrew Lock*:
  - [Creating a Quartz.NET hosted service](https://andrewlock.net/creating-a-quartz-net-hosted-service-with-asp-net-core/).
  - [Using scoped services inside a Quartz.NET hosted service](https://andrewlock.net/using-scoped-services-inside-a-quartz-net-hosted-service-with-asp-net-core/).
  - [Introducing IHostLifetime and untangling the Generic Host startup interactions](https://andrewlock.net/introducing-ihostlifetime-and-untangling-the-generic-host-startup-interactions/) *(ASP.NET Core 3.0)*.
- [System.Threading.Channels](https://docs.microsoft.com/en-us/dotnet/api/system.threading.channels?view=dotnet-plat-ext-3.0)
  - [Introduction to System.Threading.Channels](https://www.stevejgordon.co.uk/an-introduction-to-system-threading-channels) *(Steve Gordon)*
  - [Exploring System.Threading.Channels](https://ndportmann.com/system-threading-channels/) *(Nicolas Portmann)*
  - [Producer-Consumer application with Channels](https://code-maze.com/dotnet-producer-consumer-channels/) *(Code-Maze)*
- systemd for Linux: [Scott Hanselman post](https://www.hanselman.com/blog/dotnetNewWorkerWindowsServicesOrLinuxSystemdServicesInNETCore.aspx) | [Nuget package](https://www.nuget.org/packages/Microsoft.Extensions.Hosting.Systemd/)

##### Off-topic
- [Blog post](https://medium.com/cheranga/creating-and-scheduling-a-windows-service-using-topshelf-and-quartz-in-net-core-aae68b8390c) about creating and scheduling a windows service using [TopShelf](http://topshelf-project.com/) and Quartz in .NET Core.
- [Scheduled job with Worker Service](https://dev.to/jamesmh/building-a-net-core-scheduled-job-worker-service-376h) + [Coravel](https://github.com/jamesmh/coravel).
