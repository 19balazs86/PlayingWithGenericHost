# Playing with GenericHost

This small .NET Core application is an example for using the GenericHost in a console application and running background task as a Windows Service.
Note: if you use docker, you can run it without the windows service stuff.

The HostBulder is similar like in ASP.NET Core the WebHostBuilder, which allows us to configuring services, using [dependency injection](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2 "dependency injection"), configure and create [HttpClient](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2 "HttpClient"), [logging](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2 "logging") + [Serilog](https://github.com/serilog/serilog-extensions-hosting "Serilog")... but the HostBuilder do not process HTTP requests.

[TutorialDocs: The Background Tasks Based On Generic Host In .NET Core](https://www.tutorialdocs.com/article/dotnet-generic-host.html "TutorialDocs: The Background Tasks Based On Generic Host In .NET Core"), here you can find a deatiled explanation how to write background tasks, which is running all the time or just running periodically (Threading.Timer, [Quartz Scheduler](https://www.quartz-scheduler.net/ "Quartz Scheduler"), or just using [Hangfire](https://www.hangfire.io "Hangfire")).

Quick test to run as a WinService:
1. Publish with the PublushAsWinService.pubxml (publish it to folder c:\svc)
2. Run the WinServiceCommands.bat file (create, start, stop, delete functions)

**Other resources:**
- Microsoft: [.NET Generic Host](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2 ".NET Generic Host")
- Microsoft: [Background tasks with hosted services in ASP.NET Core](https://docs.microsoft.com/en-ie/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-2.2 "Background tasks with hosted services in ASP.NET Core")
- Microsoft: [Host ASP.NET Core in a Windows Service](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/windows-service?view=aspnetcore-2.2 "Microsoft: Host ASP.NET Core in a Windows Service")
- Steve Gordon: [Using HostBuilder and the Generic Host in .NET Core](https://www.stevejgordon.co.uk/using-generic-host-in-dotnet-core-console-based-microservices "Using HostBuilder and the Generic Host in .NET Core")
- Steve Gordon: [Running a .NET Core Generic Host App as a Windows Service](https://www.stevejgordon.co.uk/running-net-core-generic-host-applications-as-a-windows-service "Running a .NET Core Generic Host App as a Windows Service")
- Glenn Condron presentation: [APIs and Microservices in ASP.NET Core](https://youtu.be/dUdGcogYkss?t=1404 "APIs and Microservices in ASP.NET Core Today and Tomorrow")
