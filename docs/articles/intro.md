# Add your introductions here!


## How to add providers

# [ASP.NET Core 2.x](#tab/aspnetcore2x/)

A logging provider takes the messages that you create with an `ILogger` object and displays or stores them. For example, the Console provider displays messages on the console, and the Azure App Service provider can store them in Azure blob storage.

To use a provider, call the provider's `Add<ProviderName>` extension method in *Program.cs*:

[!code-csharp[](index/sample2/Program.cs?name=snippet_ExpandDefault&highlight=16,17)]

The default project template enables logging with the [CreateDefaultBuilder](/dotnet/api/microsoft.aspnetcore.webhost.createdefaultbuilder?view=aspnetcore-2.0#Microsoft_AspNetCore_WebHost_CreateDefaultBuilder_System_String___) method:

[!code-csharp[](index/sample2/Program.cs?name=snippet_TemplateCode&highlight=7)]

# [ASP.NET Core 1.x](#tab/aspnetcore1x/)

A logging provider takes the messages that you create with an `ILogger` object and displays or stores them. For example, the Console provider displays messages on the console, and the Azure App Service provider can store them in Azure blob storage.

To use a provider, install its NuGet package and call the provider's extension method on an instance of `ILoggerFactory`, as shown in the following example.

[!code-csharp[](index/sample//Startup.cs?name=snippet_AddConsoleAndDebug&highlight=3,5-7)]

ASP.NET Core [dependency injection](xref:fundamentals/dependency-injection) (DI) provides the `ILoggerFactory` instance. The `AddConsole` and `AddDebug` extension methods are defined in the [Microsoft.Extensions.Logging.Console](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Console/) and [Microsoft.Extensions.Logging.Debug](https://www.nuget.org/packages/Microsoft.Extensions.Logging.Debug/) packages. Each extension method calls the `ILoggerFactory.AddProvider` method, passing in an instance of the provider. 

> [!NOTE]
> The sample application for this article adds logging providers in the `Configure` method of the `Startup` class. If you want to get log output from code that executes earlier, add logging providers in the `Startup` class constructor instead. 

---

You'll find information about each [built-in logging provider](#built-in-logging-providers) and links to [third-party logging providers](#third-party-logging-providers) later in the article.

