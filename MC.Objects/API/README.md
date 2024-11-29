# MandalaConsulting.Objects

## Overview

The **MandalaConsulting.Objects** library provides a simple and reusable framework for encapsulating response data objects in .NET applications. It includes pre-built classes like `ResponseData` to standardize the structure of API responses or other object-oriented data exchange patterns.

---

## Features

- **Lightweight**: A minimal library designed for quick integration.
- **Reusability**: Provides standardized object structures for data exchange.
- **Flexibility**: Easily adaptable for a wide variety of use cases.

---

## Installation

Simply include the library in your .NET project. Ensure that you reference the `MandalaConsulting.Objects` namespace in your code.

---

## Usage

### Import the Namespace
```csharp
using MandalaConsulting.Objects;
```

### ResponseData Class

The `ResponseData` class is a flexible container for passing structured data between layers or systems.

#### Constructors

1. **Default Constructor**
   ```csharp
   ResponseData responseData = new ResponseData();
   ```

2. **Parameterized Constructor**
   ```csharp
   ResponseData responseData = new ResponseData("Success", someData, null);
   ```

#### Properties

- **`Data`**: Contains the main payload (e.g., result of an operation).
- **`Error`**: Stores error details, if any.
- **`message`**: Holds a status message or description.

#### Example Usage

```csharp
var response = new ResponseData
{
    message = "Operation completed successfully",
    Data = new { Id = 1, Name = "John Doe" },
    Error = null
};

// Access properties
Console.WriteLine(response.message);  // Output: Operation completed successfully
Console.WriteLine(response.Data);    // Output: { Id = 1, Name = "John Doe" }
```

---

## Requirements

- .NET Framework or .NET Core
- No additional dependencies required.

---

## License

This project is copyrighted © 2023 Mandala Consulting, LLC.  
**All Rights Reserved**.

---

## Author

Created by **Alexander Fields**  
For inquiries, please contact [Mandala Consulting](https://mandalaconsulting.com).
