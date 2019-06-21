# Playing with GenericHost

This small .NET Core application is an example for using the GenericHost in a console application and running background task as a Windows Service.
Note: if you use docker, you can run it without the windows service stuff.

#### What is the HostBulder?

- HostBuilder is a lightweight version of the WebHostBuilder because it does not process HTTP requests.
- HostBuilder similarly like the WebHostBuilder, allows us to configure services, using [dependency injection](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2 "dependency injection"), [HttpClientFactory](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2 "HttpClientFactory"), [logging](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2 "logging") + [Serilog](https://github.com/serilog/serilog-extensions-hosting "Serilog") and so on.

#### Run this application as a WinService

1. Publish the application with the predefined PublishAsWinService.pubxml (c:\svc)
2. Run the WinServiceCommands.bat file (create, start, stop, delete functions)

#### Resources

- [TutorialDocs: The Background Tasks Based On Generic Host In .NET Core](https://www.tutorialdocs.com/article/dotnet-generic-host.html "TutorialDocs: The Background Tasks Based On Generic Host In .NET Core"), here you can find a deatiled explanation how to write background tasks, which is running all the time or just running periodically (Threading.Timer, [Quartz Scheduler](https://www.quartz-scheduler.net/ "Quartz Scheduler"), or using [Hangfire](https://www.hangfire.io "Hangfire")).
- Microsoft:
  - [.NET Generic Host](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2 ".NET Generic Host").
  - [Background tasks with hosted services in ASP.NET Core](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-2.2 "Background tasks with hosted services in ASP.NET Core").
  - [Host ASP.NET Core in a Windows Service](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-2.2 "Microsoft: Host ASP.NET Core in a Windows Service").
- .Net Tutorials: [Hosted Services In ASP.NET Core](https://dotnetcoretutorials.com/2019/01/13/hosted-services-in-asp-net-core "Hosted Services In ASP.NET Core").
- Blog posts from Steve Gordon:
  - [Using HostBuilder and the Generic Host in .NET Core](https://www.stevejgordon.co.uk/using-generic-host-in-dotnet-core-console-based-microservices "Using HostBuilder and the Generic Host in .NET Core").
  - [Running a .NET Core Generic Host App as a Windows Service](https://www.stevejgordon.co.uk/running-net-core-generic-host-applications-as-a-windows-service "Running a .NET Core Generic Host App as a Windows Service").
- Glenn Condron presentation: [APIs and Microservices in ASP.NET Core](https://youtu.be/dUdGcogYkss?t=1404 "APIs and Microservices in ASP.NET Core Today and Tomorrow").
- Blog posts from Andrew Lock:
  - [Creating a Quartz.NET hosted service](https://andrewlock.net/creating-a-quartz-net-hosted-service-with-asp-net-core/ "Creating a Quartz.NET hosted service").
  - [Using scoped services inside a Quartz.NET hosted service](https://andrewlock.net/using-scoped-services-inside-a-quartz-net-hosted-service-with-asp-net-core/ "Using scoped services inside a Quartz.NET hosted service").

##### Off-topic
- [Blog post](https://medium.com/cheranga/creating-and-scheduling-a-windows-service-using-topshelf-and-quartz-in-net-core-aae68b8390c "Blog post") about creating and scheduling a windows service using [TopShelf](http://topshelf-project.com/ "TopShelf") and Quartz in .NET Core.
- [.NET Core 3 scheduled job with Worker Service](https://dev.to/jamesmh/building-a-net-core-scheduled-job-worker-service-376h ".NET Core 3 scheduled job with Worker Service") + [Coravel](https://github.com/jamesmh/coravel "Coravel").
