[← Back to Dictionary](./PROJECT_STRUCTURE_DICTIONARY.md)

# PROJECT STRUCTURE LIBRARIES

## Solution Overview
The MandalaConsulting solution contains 6 core libraries targeting .NET 8.0, each focusing on specific functionality areas. All libraries are designed as NuGet packages with MIT licensing.

## Library Details

### 1. MC.Logging (MandalaConsulting.Logging) v0.0.5

**Purpose & Functionality:**
- Provides standardized logging framework for .NET applications
- Event-driven logging system with predefined message types
- Advanced async method name resolution for better debugging

**Key Classes:**
- `LogMessage` - Core logging object with automatic source tracking
- `MessageType` enum - Defines log levels (Error, Warning, Success, Informational, Message, Critical, Celebrate)
- `LogMessageEventArgs` - Event arguments for log events

**Dependencies:**
- **Internal:** None (foundational library)
- **External:** .NET 8.0 framework only

**Public API Surface:**
- Static factory methods: `LogMessage.Error()`, `LogMessage.Warning()`, etc.
- Event system: `LogMessage.LogAdded` event
- Properties: `id`, `localOperationName`, `message`, `messageSource`, `messageType`, `timeStamp`
- Static property: `MessageSourceSetter` for application identification

### 2. MC.Memory (MandalaConsulting.Memory) v0.0.4

**Purpose & Functionality:**
- Memory management utilities for .NET applications
- Controlled garbage collection with OS memory return
- Performance optimization through manual GC triggers

**Key Classes:**
- `GarbageCollection` - Main class for memory management operations

**Dependencies:**
- **Internal:** None
- **External:** .NET 8.0 framework only

**Public API Surface:**
- `PerformGarbageCollection(object state)` - Triggers GC and memory cleanup
- `ReturnUnusedMemoryToOS()` - Private method for OS memory return
- Uses .NET's no-GC region feature for optimization

### 3. MC.APIMiddlewares (MandalaConsulting.APIMiddlewares) v0.0.13

**Purpose & Functionality:**
- ASP.NET Core middleware collection for API security and monitoring
- IP blacklisting, endpoint tracking, and API key validation
- Memory management integration for idle endpoints

**Key Classes:**
- `IPBlacklistMiddleware` - Blocks malicious IPs
- `InvalidEndpointTrackerMiddleware` - Tracks failed endpoint attempts
- `EndpointAccessMiddleware` - Monitors endpoint usage and manages memory
- `APIKeyAttribute` - API key validation filter
- `BannedIP` - Data model for blocked IPs
- `IPBlacklist` - Static IP management class
- `APIUtility` - IP address extraction utilities

**Dependencies:**
- **Internal:** 
  - MC.Logging (logging functionality)
  - MC.Memory (garbage collection)
- **External:**
  - Microsoft.AspNetCore (2.3.0)
  - Microsoft.AspNetCore.Mvc.Core (2.3.0)

**Public API Surface:**
- Middleware registration methods
- Static methods: `IPBlacklist.AddBannedIP()`, `IPBlacklist.IsIPBlocked()`
- Event system for logging and IP banning
- Environment variable configuration: `API_KEY`, `API_KEY_NAME`

### 4. MC.Repository.Mongo (MandalaConsulting.Repository.Mongo) v0.0.5

**Purpose & Functionality:**
- MongoDB database operations wrapper
- Generic CRUD operations with strongly-typed interfaces
- Connection management and logging integration

**Key Classes:**
- `IMongoHelper` - Interface defining MongoDB operations
- `MongoHelper` - Implementation of MongoDB operations
- `MongoHelperFactory` - Factory for creating configured instances

**Dependencies:**
- **Internal:**
  - MC.Logging (for operation logging)
- **External:**
  - MongoDB.Bson (3.4.0)
  - MongoDB.Driver (3.4.0)
  - MongoDB.Driver.Core (2.30.0) 
  - MongoDB.Driver.GridFS (2.30.0)
  - MongoDB.Libmongocrypt (1.12.0)

**Public API Surface:**
- CRUD operations: `CreateDocumentAsync<T>()`, `GetAllDocumentsAsync<T>()`, `UpdateDocumentAsync<T>()`, `DeleteDocumentAsync<T>()`
- Connection management: `CreateMongoDbInstance()`, `TestConnection()`
- Utility methods: `ConnectionStringBuilder()`, `GetIdFromObj<T>()`
- Event system: `LogAdded`, `LogCleared` events

### 5. MC.Objects (MandalaConsulting.Objects) v0.0.12

**Purpose & Functionality:**
- Core data models for general application development
- Account management and billing system models
- MongoDB and JSON serialization support

**Key Components:**

**Account Namespace:**
- `User` - User account with authentication and profile data
- `Profile` - User profile information
- `Credentials` - Authentication credentials
- `IPInfo` - IP address and location tracking

**Billing Namespace:**
- `Product`, `Purchase`, `Sale` - Product and transaction models
- `Subscription` - Recurring billing
- `Bill` - Invoice generation
- `CreditCard`, `PaymentCredentials`, `PaymentType` - Payment processing
- `Address`, `Contact` - Contact information
- `Inventory` - Inventory tracking

**Core Classes:**
- `ResponseData` - Generic response wrapper
- `JObjectSerializer` - Custom BSON/JSON serialization

**Dependencies:**
- **Internal:** None
- **External:**
  - MongoDB.Driver (3.4.0)
  - MongoDB.Driver.Core (2.30.0)
  - MongoDB.Bson (3.4.0)
  - MongoDB.Driver.GridFS (2.30.0)
  - MongoDB.Libmongocrypt (1.12.0)
  - Newtonsoft.Json (13.0.3)

### 6. MC.Objects.API (MandalaConsulting.Objects.API) v0.0.5

**Purpose & Functionality:**
- API-specific data models extending MC.Objects
- ASP.NET Core integration for web APIs
- File upload handling and API response standardization

**Key Classes:**
- All classes from MC.Objects (same structure, different namespace)
- `CustomFormFile` - IFormFile implementation for multipart uploads
- API-specific `ResponseData` class

**Dependencies:**
- **Internal:** None (contains duplicate models from MC.Objects)
- **External:**
  - MongoDB.Driver (3.4.0)
  - MongoDB.Driver.Core (2.30.0)
  - MongoDB.Bson (3.4.0)
  - MongoDB.Driver.GridFS (2.30.0)
  - MongoDB.Libmongocrypt (1.12.0)
  - Microsoft.AspNetCore.Mvc.NewtonsoftJson (8.0.17)
  - Newtonsoft.Json (13.0.3)

## Inter-Library Dependencies

```
MC.APIMiddlewares
├── MC.Logging (logging)
└── MC.Memory (garbage collection)

MC.Repository.Mongo
└── MC.Logging (operation logging)

MC.Memory
└── (no internal dependencies)

MC.Logging
└── (no internal dependencies)

MC.Objects
└── (no internal dependencies)

MC.Objects.API
└── (no internal dependencies)
```

## Key Architectural Notes

1. **Namespace Structure**: Libraries use `MandalaConsulting.{Feature}` pattern with sub-namespaces
2. **Duplicate Models**: MC.Objects and MC.Objects.API contain identical models in different namespaces
3. **MongoDB Integration**: Heavy use of MongoDB across multiple libraries
4. **Event-Driven Architecture**: Logging and middleware components use .NET events
5. **NuGet Package Strategy**: All libraries configured for NuGet distribution
6. **Versioning**: Libraries are at different version stages (0.0.4 to 0.0.13)