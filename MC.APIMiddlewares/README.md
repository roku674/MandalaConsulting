# MandalaConsulting.APIMiddleware

## Overview

The **MandalaConsulting.APIMiddleware** library is a comprehensive middleware solution for ASP.NET Core applications. It provides tools for security, performance optimization, and API monitoring. Features include IP blacklisting, invalid endpoint tracking, memory management, and API key validation, designed to enhance the reliability and security of your APIs.

---

## Features

### Security and Monitoring
- **IP Blacklisting**: Automatically blocks malicious IPs after repeated unauthorized attempts.
- **Invalid Endpoint Tracking**: Monitors failed attempts to access non-existent endpoints and prevents abuse.
- **API Key Validation**: Ensures requests are authorized with the correct API key.
- **Logging**: Captures detailed logs of unauthorized access attempts and API interactions.

### Performance Optimization
- **Memory Management**: Includes garbage collection based on endpoint usage patterns.
- **Idle Endpoint Detection**: Identifies idle endpoints to optimize resource usage.

### Flexible Middleware
- Designed to be easily integrated into any ASP.NET Core application.

---

## Installation

Add the library to your ASP.NET Core project and reference the necessary namespaces:

```csharp
using MandalaConsulting.APIMiddleware;
using MandalaConsulting.APIMiddleware.Filters;
using MandalaConsulting.APIMiddleware.Utility;
using MandalaConsulting.Optimization.Logging;
```

---

## Usage

### Middleware Components

#### **InvalidEndpointTrackerMiddleware**

Tracks attempts to access invalid endpoints and blocks malicious activity.

##### Example Integration

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseMiddleware<InvalidEndpointTrackerMiddleware>();
}
```

---

#### **IPBlacklistMiddleware**

Blocks requests from IPs flagged for malicious activity.

##### Example Integration

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseMiddleware<IPBlacklistMiddleware>();
}
```

##### Get Blocked IPs Log

```csharp
var logs = IPBlacklistMiddleware.GetLogs();
foreach (var log in logs)
{
    Console.WriteLine(log.message);
}
```

---

#### **EndpointAccessMiddleware**

Monitors endpoint activity and performs memory cleanup if endpoints remain idle.

##### Example Integration

```csharp
public void Configure(IApplicationBuilder app)
{
    app.UseMiddleware<EndpointAccessMiddleware>(TimeSpan.FromMinutes(5), true);
}
```

---

### Filters

#### **APIKeyAttribute**

Validates requests with an API key stored in environment variables.

##### Example Usage

Apply the attribute to a controller or action:

```csharp
[APIKey]
[ApiController]
[Route("[controller]")]
public class SecureController : ControllerBase
{
    [HttpGet]
    public IActionResult GetData()
    {
        return Ok("Secure Data Accessed");
    }
}
```

##### Environment Variables Required
- `API_KEY`: Your API key.
- `API_KEY_NAME`: Header name for the API key.

---

## Security Features

### IP Blocking
IP addresses are automatically blocked after exceeding the threshold for unauthorized attempts.

```csharp
IPBlacklist.AddBannedIP("192.168.1.1", "Repeated unauthorized attempts.");
```

---

## Logging

All middleware components use the logging system provided by `MandalaConsulting.Optimization.Logging`.

### Add Logs

```csharp
IPBlacklistMiddleware.AddLog(LogMessage.Warning("Suspicious activity detected."));
```

### Retrieve Logs

```csharp
var logs = IPBlacklistMiddleware.GetLogs();
foreach (var log in logs)
{
    Console.WriteLine(log.message);
}
```

### Clear Logs

```csharp
IPBlacklistMiddleware.ClearLogs();
```

---

## Requirements

- .NET Core 3.1 or later
- ASP.NET Core

---

## License

This project is copyrighted © 2024 Mandala Consulting, LLC.  
**All Rights Reserved**.

---

## Author

Created by **Alexander Fields**  
For inquiries, please contact [Mandala Consulting](https://mandalaconsulting.com).
