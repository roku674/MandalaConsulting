[← Back to Dictionary](./PROJECT_STRUCTURE_DICTIONARY.md)

# PROJECT STRUCTURE TESTING

## Test Organization
All test projects are organized under a "Tests" solution folder in the main .sln file.

## Test Projects Structure

### 1. MC.Logging.Tests (MandalaConsulting.Logging.Tests)
**Test Files:**
- `LogMessageTests.cs` - Tests for LogMessage class
- `MessageTypeTests.cs` - Tests for MessageType enum

**Testing Framework:** MSTest (.NET 8.0)

### 2. MC.Memory.Tests (MandalaConsulting.Memory.Tests)
**Test Files:**
- `GarbageCollectionTests.cs` - Tests for GarbageCollection class

**Testing Framework:** MSTest (.NET 8.0)

### 3. MC.APIMiddlewares.Tests (MandalaConsulting.APIMiddlewares.Tests)
**Test Files:**
- `EndpointAccessMiddlewareTests.cs` - Tests for endpoint access middleware
- `IPBlacklistMiddlewareTests.cs` - Tests for IP blacklist middleware
- `InvalidEndpointTrackerMiddlewareTests.cs` - Tests for endpoint tracking
- `AttemptInfoTests.cs` - Tests for attempt tracking objects

**Subdirectories:**
- `Filters/APIKeyAttributeTests.cs` - Tests for API key validation filter
- `Objects/BannedIPTests.cs` - Tests for BannedIP objects
- `Objects/IPBlacklistTests.cs` - Tests for IPBlacklist functionality
- `Utility/APIUtilityTests.cs` - Tests for API utility methods

**Testing Framework:** MSTest (.NET 8.0)

### 4. MC.Repository.Mongo.Tests (MandalaConsulting.Repository.Mongo.Tests)
**Test Files:**
- `MongoHelperTests.cs` - Tests for MongoDB operations

**Testing Framework:** MSTest (.NET 8.0)

### 5. Missing Test Projects
The following libraries do not have corresponding test projects:
- **MC.Objects** - No test project found
- **MC.Objects.API** - No test project found

## Test Patterns and Standards

### Naming Conventions
- Test projects follow pattern: `{LibraryName}.Tests`
- Test files follow pattern: `{ClassUnderTest}Tests.cs`
- Test methods typically use descriptive names

### Framework Consistency
- All existing test projects use **MSTest** framework
- All target **.NET 8.0** framework
- Consistent project structure with `bin/` and `obj/` directories

### Test Coverage
- **Well-covered libraries:**
  - MC.Logging (2 test files)
  - MC.APIMiddlewares (7 test files across subdirectories)
  
- **Minimal coverage:**
  - MC.Memory (1 test file)
  - MC.Repository.Mongo (1 test file)
  
- **No coverage:**
  - MC.Objects (0 test files)
  - MC.Objects.API (0 test files)

## Testing Gaps and Recommendations

### Missing Test Projects
1. **MC.Objects.Tests** - Should test all account and billing models
2. **MC.Objects.API.Tests** - Should test API-specific functionality and CustomFormFile

### Potential Test Improvements
1. **MC.Memory.Tests** - Could use more comprehensive memory testing scenarios
2. **MC.Repository.Mongo.Tests** - Could benefit from integration tests with actual MongoDB instances
3. **All projects** - Could benefit from integration testing between libraries that depend on each other

## Build and Test Commands
Based on the .NET nature of the project:
- Build: `~/.dotnet/dotnet build`
- Test: `~/.dotnet/dotnet test`
- Individual project testing: `~/.dotnet/dotnet test MC.{LibraryName}.Tests/`