# MongoHelper

## Overview

The **MongoHelper** library, developed by Mandala Consulting, LLC, provides a simple and efficient wrapper for MongoDB database operations in .NET. It offers utility methods to handle common MongoDB operations such as creating, reading, updating, and deleting documents, alongside helper functions for managing connections and logging.

---

## Features

- Simplifies MongoDB connection setup.
- CRUD operations for MongoDB documents:
  - Create: Insert documents.
  - Read: Fetch documents using filters or retrieve all documents.
  - Update: Modify existing documents.
  - Delete: Remove documents based on filters.
- Logging functionality to capture MongoDB activity.
- Event-driven log management (`LogAdded`, `LogCleared`).
- Connection string builder for secure MongoDB access.
- Helper methods to extract document IDs dynamically.

---

## Installation

Clone the repository and include the library in your project. Ensure you have the following dependencies installed:

- `MongoDB.Driver`
- `MandalaConsulting.Optimization.Logging`

---

## Usage

### Setting Up MongoHelper

```csharp
using MandalaConsulting.Repository.Mongo;

// Initialize with database name and connection string
MongoHelper mongoHelper = new MongoHelper("YourDatabaseName", "YourConnectionString");
```

### Connection String Builder

You can dynamically create a secure connection string:

```csharp
string connectionString = MongoHelper.ConnectionStringBuilder(
    "username",
    "password",
    "clusterName",
    "region"
);
```

### Basic CRUD Operations

#### Create a Document
```csharp
await mongoHelper.CreateDocumentAsync("CollectionName", new { Name = "John Doe", Age = 30 });
```

#### Read Documents
Fetch all documents:
```csharp
var documents = await mongoHelper.GetAllDocumentsAsync<MyDocument>("CollectionName");
```

Fetch documents with a filter:
```csharp
var filter = Builders<MyDocument>.Filter.Eq(doc => doc.Name, "John Doe");
var filteredDocuments = await mongoHelper.GetFilteredDocumentsAsync("CollectionName", filter);
```

#### Update a Document
```csharp
var filter = Builders<MyDocument>.Filter.Eq(doc => doc.Id, 1);
var update = Builders<MyDocument>.Update.Set(doc => doc.Name, "Jane Doe");
await mongoHelper.UpdateDocumentAsync("CollectionName", filter, update);
```

#### Delete a Document
```csharp
var filter = Builders<MyDocument>.Filter.Eq(doc => doc.Id, 1);
await mongoHelper.DeleteDocumentAsync("CollectionName", filter);
```

### Testing Database Connection
```csharp
List<string> collections = mongoHelper.TestConnection();
if (collections != null)
{
    Console.WriteLine("Connection successful!");
}
```

---

## Events

### `LogAdded` Event

Triggered when a new log is added:

```csharp
MongoHelper.LogAdded += (sender, args) =>
{
    Console.WriteLine($"New Log: {args.LogMessage}");
};
```

### `LogCleared` Event

Triggered when logs are cleared:

```csharp
MongoHelper.LogCleared += (sender, args) =>
{
    Console.WriteLine("Logs cleared.");
};
```

---

## Logging

### Retrieve Logs
```csharp
var logs = MongoHelper.GetLogs();
foreach (var log in logs)
{
    Console.WriteLine(log.Message);
}
```

### Clear Logs
```csharp
MongoHelper.ClearLogs();
```

---

## Requirements

- .NET Framework or .NET Core
- MongoDB Instance
- NuGet Packages:
  - MongoDB.Driver
  - MandalaConsulting.Optimization.Logging

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## Author

Created by **Alexander Fields**  
Copyright © 2023 Mandala Consulting, LLC  

For inquiries, please contact [Mandala Consulting](https://mandalaconsulting.com).
