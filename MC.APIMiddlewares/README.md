# MC.APIMiddlewares

## Overview

MC.APIMiddlewares provides a set of middleware components designed to enhance the functionality of ASP.NET Core applications. These components help manage various cross-cutting concerns, improving code modularity and maintainability.

## Features

- **IP Blacklist Middleware**: Blocks requests from specified IP addresses.
- **Invalid Endpoint Tracker Middleware**: Logs attempts to access invalid API endpoints.

## Installation

You can install the package via NuGet:

```bash
dotnet add package MC.APIMiddlewares
```

## Usage

To use the middlewares, add them in the `Configure` method of your `Startup.cs`:

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseMiddleware<IPBlacklistMiddleware>();
    app.UseMiddleware<InvalidEndpointTrackerMiddleware>();
}
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any enhancements or bug fixes.

## Liscence
