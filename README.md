# Playing with GenericHost
This console application is an example of using the `GenericHost` as known `WorkerService` (from .NET Core 3). Running background task as a Windows Service.

[Separate branch](https://github.com/19balazs86/PlayingWithGenericHost/tree/netcoreapp2.2) with the .NET Core 2.2 version.

#### What is the HostBulder?
- `HostBuilder` is a lightweight version of the `WebHostBuilder` and it does not process HTTP requests.
- `HostBuilder` similarly like the `WebHostBuilder`, allows to configure services, using [dependency injection](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.0), [HttpClientFactory](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/http-requests?view=aspnetcore-3.0), [logging](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/logging/?view=aspnetcore-3.0) + [Serilog](https://github.com/serilog/serilog-extensions-hosting) and so on.

#### Run this application as a Windows Service
1. Publish the application with the predefined PublishAsWinService.pubxml (c:\svc)
2. Run the WinServiceCommands.bat file (create, start, stop, delete functions)

#### Examples
- PrinterService: a simple example of using `BackgroundService` as a base class.
- FileWriterService: a simple example of using `IHostedService` and a Timer in it.
- `Quartz`: using the Quartz.NET to create background process timing with cron expression.
- `System.Threading.Channels`: leverage this library to create a bounded channel/in-memory queue. Using IAsyncEnumerable.

#### Resources
- TutorialDocs: [Background Tasks based on Generic Host in .NET Core](https://www.tutorialdocs.com/article/dotnet-generic-host.html), you can find a detailed explanation of how to write background tasks, which is running all the time or just running periodically (Threading.Timer, [Quartz Scheduler](https://www.quartz-scheduler.net/), or using [Hangfire](https://www.hangfire.io)).
- Microsoft:
  - [.NET Generic Host](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0).
  - [Background tasks with hosted services in ASP.NET Core](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.0).
  - [Host ASP.NET Core in a Windows Service](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-3.0).
- Telerik blog: [.NET Core 3 Background Services](https://www.telerik.com/blogs/.net-core-background-services).
- .Net Tutorials: [Hosted Services In ASP.NET Core](https://dotnetcoretutorials.com/2019/01/13/hosted-services-in-asp-net-core).
- Glenn Condron: [Community Standup - Takes Workers Everywhere](https://www.youtube.com/watch?v=5AEqA035o5I&feature=youtu.be&t=1709) + Docker example.
- Blog posts from *Andrew Lock*:
  - [Creating a Quartz.NET hosted service](https://andrewlock.net/creating-a-quartz-net-hosted-service-with-asp-net-core/).
  - [Using scoped services inside a Quartz.NET hosted service](https://andrewlock.net/using-scoped-services-inside-a-quartz-net-hosted-service-with-asp-net-core/).
  - [Introducing IHostLifetime and untangling the Generic Host startup interactions](https://andrewlock.net/introducing-ihostlifetime-and-untangling-the-generic-host-startup-interactions/) *(ASP.NET Core 3.0)*.
- [System.Threading.Channels](https://docs.microsoft.com/en-us/dotnet/api/system.threading.channels?view=dotnet-plat-ext-3.0)
  - [Introduction to System.Threading.Channels](https://www.stevejgordon.co.uk/an-introduction-to-system-threading-channels) *(Steve Gordon)*
  - [Exploring System.Threading.Channels](https://ndportmann.com/system-threading-channels/) *(Nicolas Portmann)*

##### Off-topic
- [Blog post](https://medium.com/cheranga/creating-and-scheduling-a-windows-service-using-topshelf-and-quartz-in-net-core-aae68b8390c) about creating and scheduling a windows service using [TopShelf](http://topshelf-project.com/) and Quartz in .NET Core.
- [Scheduled job with Worker Service](https://dev.to/jamesmh/building-a-net-core-scheduled-job-worker-service-376h) + [Coravel](https://github.com/jamesmh/coravel).
