# MandalaConsulting.Objects.API

## Overview

The **MandalaConsulting.Objects.API** library extends the functionality of **MandalaConsulting.Objects** by providing additional object models and utilities tailored for ASP.NET Core applications. It is designed to standardize and simplify the management of HTTP responses and interactions within ASP.NET Core APIs.

---

## Features

- **ASP.NET Core Integration**: Optimized for use with ASP.NET Core's HTTP request/response pipeline.
- **Standardized API Responses**: Includes enhanced models like `ApiResponse` for consistent communication.
- **Flexible Data Structures**: Easily adaptable to a wide variety of API use cases.
- **Lightweight and Reusable**: Minimal overhead, designed for plug-and-play integration.

---

## Installation

To use **MandalaConsulting.Objects.API**, include it in your .NET project and ensure the following packages are available:

- `Microsoft.AspNetCore.Http`
- `MandalaConsulting.Objects`

---

## Usage

### Import the Namespace

```csharp
using MandalaConsulting.Objects.API;
```

---

### Core Classes

#### **ApiResponse**

The `ApiResponse` class provides a structured format for HTTP API responses, encapsulating data, metadata, and error information.

##### Properties

- **`StatusCode`**: The HTTP status code associated with the response.
- **`Message`**: A descriptive message about the response.
- **`Data`**: The main payload of the response.
- **`Error`**: Details about any errors encountered.

##### Constructors

1. **Default Constructor**
   ```csharp
   ApiResponse response = new ApiResponse();
   ```

2. **Parameterized Constructor**
   ```csharp
   ApiResponse response = new ApiResponse(200, "Success", new { Id = 1, Name = "John Doe" }, null);
   ```

##### Example Usage

```csharp
var response = new ApiResponse
{
    StatusCode = StatusCodes.Status200OK,
    Message = "Data retrieved successfully",
    Data = new { Id = 1, Name = "John Doe" },
    Error = null
};

// Serialize to JSON for API output
string jsonResponse = JsonSerializer.Serialize(response);
```

---

#### **ErrorResponse**

The `ErrorResponse` class is designed to encapsulate detailed error information in API responses.

##### Properties

- **`StatusCode`**: The HTTP status code indicating the type of error.
- **`Error`**: A descriptive error message or object.
- **`Details`**: Additional details about the error, such as stack trace or validation errors.

##### Example Usage

```csharp
var errorResponse = new ErrorResponse
{
    StatusCode = StatusCodes.Status400BadRequest,
    Error = "Invalid input data",
    Details = new { MissingFields = new[] { "Name", "Email" } }
};

// Serialize to JSON for API output
string errorJson = JsonSerializer.Serialize(errorResponse);
```

---

### Middleware Integration

You can use **MandalaConsulting.Objects.API** to standardize error handling and responses in your ASP.NET Core middleware.

#### Example: Global Exception Handling

```csharp
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var errorResponse = new ErrorResponse
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Error = "An unexpected error occurred",
            Details = ex.Message
        };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
});
```

---

## Requirements

- .NET Core 3.1 or later
- ASP.NET Core
- Dependencies:
  - `Microsoft.AspNetCore.Http`
  - `System.Text.Json` (or any JSON serialization library)

---

## License

This project is copyrighted © 2023 Mandala Consulting, LLC.  
**All Rights Reserved**.

---

## Author

Created by **Alexander Fields**  
For inquiries, please contact [Mandala Consulting](https://mandalaconsulting.com).
