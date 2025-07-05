[← Back to Dictionary](./PROJECT_STRUCTURE_DICTIONARY.md)

# PROJECT STRUCTURE OVERVIEW

## Architecture
This is a .NET solution containing multiple NuGet package libraries developed by Mandala Consulting.

## Technology Stack
- **.NET**: Target framework for all projects
- **C#**: Primary programming language
- **Visual Studio**: Solution format v12.00
- **NuGet**: Package distribution
- **GitHub Actions**: CI/CD for automatic publishing
- **MongoDB**: Database integration via Repository pattern

## Solution Structure
The solution `MandalaConsulting.sln` contains the following project organization:

### Main Libraries
1. **MandalaConsulting.Logging** (`MC.Logging/`)
2. **MandalaConsulting.APIMiddlewares** (`MC.APIMiddlewares/`)
3. **MandalaConsulting.Repository.Mongo** (`MC.Repository.Mongo/`)
4. **MandalaConsulting.Objects.API** (`MC.Objects.API/`)
5. **MandalaConsulting.Memory** (`MC.Memory/`)
6. **MandalaConsulting.Objects** (`MC.Objects/`)

### Test Projects
All test projects are organized under a "Tests" solution folder:
- **MandalaConsulting.Logging.Tests** (`MC.Logging.Tests/`)
- **MandalaConsulting.Memory.Tests** (`MC.Memory.Tests/`)
- **MandalaConsulting.APIMiddlewares.Tests** (`MC.APIMiddlewares.Tests/`)
- **MandalaConsulting.Repository.Mongo.Tests** (`MC.Repository.Mongo.Tests/`)

### External Dependencies
- **Google/** - Contains Google OAuth integration objects

## Publishing Strategy
- **Stable Releases**: Published from `master` branch
- **Pre-releases**: Published from `develop` branch with `-develop.BUILD` suffix
- Automated via GitHub Actions on branch merges
- Manual publishing via PowerShell/shell scripts available

## Development Workflow
- Development occurs on `develop` branch
- Feature branches created from `develop`
- Merges to `master` trigger stable releases
- Clean build and test requirements before commits