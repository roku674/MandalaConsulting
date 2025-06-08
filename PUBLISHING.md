# Publishing MandalaConsulting NuGet Packages

This repository contains several NuGet packages that can be published to NuGet.org:

- [MandalaConsulting.APIMiddlewares](https://www.nuget.org/packages/MandalaConsulting.APIMiddlewares/)
- [MandalaConsulting.Logging](https://www.nuget.org/packages/MandalaConsulting.Logging/)
- [MandalaConsulting.Memory](https://www.nuget.org/packages/MandalaConsulting.Memory/)
- [MandalaConsulting.Objects](https://www.nuget.org/packages/MandalaConsulting.Objects/)
- [MandalaConsulting.Objects.API](https://www.nuget.org/packages/MandalaConsulting.Objects.API/)
- [MandalaConsulting.Repository.Mongo](https://www.nuget.org/packages/MandalaConsulting.Repository.Mongo/)

## Prerequisites

1. .NET 8.0 SDK installed
2. NuGet API key from nuget.org
3. All tests passing

## Publishing Scripts

Three scripts are provided for different platforms:

### Windows (Batch)
```batch
publish-packages.bat YOUR_NUGET_API_KEY [version-increment]
```

### Windows (PowerShell)
```powershell
.\publish-packages.ps1 -NuGetApiKey YOUR_NUGET_API_KEY [-VersionIncrement patch|minor|major] [-SkipTests] [-DryRun]
```

### Linux/Mac (Bash)
```bash
./publish-packages.sh YOUR_NUGET_API_KEY [version-increment]
```

## Version Increment Options

- `patch` (default): 1.0.0 → 1.0.1
- `minor`: 1.0.0 → 1.1.0
- `major`: 1.0.0 → 2.0.0

## What the Scripts Do

1. **Clean** previous builds
2. **Restore** all dependencies
3. **Build** the solution in Release mode
4. **Run** all tests (can be skipped with PowerShell -SkipTests flag)
5. **Update** version numbers (if version increment specified)
6. **Pack** each project into a NuGet package
7. **Push** packages to NuGet.org

## PowerShell Script Features

The PowerShell script has additional features:

- **DryRun mode**: Test the process without actually publishing
- **Skip tests**: Skip test execution (not recommended for production)
- **Detailed output**: Color-coded success/failure messages
- **Automatic version management**: Updates .csproj files with new versions

## Examples

### Publish with patch version increment:
```powershell
# PowerShell
.\publish-packages.ps1 -NuGetApiKey YOUR_NUGET_API_KEY

# Bash
./publish-packages.sh YOUR_NUGET_API_KEY
```

### Publish with minor version increment:
```powershell
# PowerShell
.\publish-packages.ps1 -NuGetApiKey YOUR_NUGET_API_KEY -VersionIncrement minor

# Bash
./publish-packages.sh YOUR_NUGET_API_KEY minor
```

### Dry run to see what would happen:
```powershell
.\publish-packages.ps1 -NuGetApiKey YOUR_NUGET_API_KEY -DryRun
```

## Manual Publishing

If you prefer to publish packages manually:

```bash
# Build
dotnet build --configuration Release

# Test
dotnet test --configuration Release

# Pack a specific project
dotnet pack MC.Logging/MandalaConsulting.Logging.csproj --configuration Release

# Push to NuGet
dotnet nuget push MC.Logging/bin/Release/MandalaConsulting.Logging.*.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
```

## GitHub Actions

The repository also has GitHub Actions that automatically publish packages when:
- Pushing to master or develop branches
- Merging pull requests

Make sure the `NUGET_API_KEY` secret is set in your repository settings.