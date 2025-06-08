# MandalaConsulting Project Notes

## NuGet Publishing
- **NuGet API Key**: Stored as GitHub secret `NUGET_API_KEY`
- This key is used in GitHub Actions for automated package publishing
- Key value: [REDACTED - stored securely in GitHub secrets]

## Project Structure
- Internal dependencies use project references, not package references
- All projects target .NET 8.0
- ASP.NET Core version is 2.3.0 (do not use features from 3.0+)

## Build and Test Commands
```bash
# Build
dotnet build --configuration Release

# Run all tests
dotnet test --configuration Release --verbosity minimal

# Run specific test project
dotnet test MC.Logging.Tests/MandalaConsulting.Logging.Tests.csproj --no-build --configuration Release
```

## CI/CD Notes
- GitHub Actions workflows target both master and develop branches
- Dependabot is configured to create PRs against develop branch
- NuGet packages are automatically published on push to master/develop