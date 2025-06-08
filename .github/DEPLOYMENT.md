# Deployment Strategy

## Overview

This repository uses different deployment strategies for stable releases and pre-releases:

### 🏷️ Stable Releases (master branch)
- **Trigger**: Push to `master` branch or GitHub Release creation
- **Workflow**: `nuget-publish-release.yml`
- **Version**: Uses exact version from `.csproj` files (e.g., `1.0.0`)
- **Target**: Production use

### 🚧 Pre-releases (develop branch)
- **Trigger**: Push to `develop` branch
- **Workflow**: `nuget-publish-prerelease.yml`
- **Version**: Adds prerelease suffix (e.g., `1.0.0-develop.123`)
- **Target**: Testing and early adoption
- **Note**: Only publishes packages that have changed

## Branching Strategy

```
master (stable releases)
   ↑
   | (merge when ready for release)
   |
develop (pre-releases)
   ↑
   | (merge features)
   |
feature/* branches
```

## Release Process

### For Stable Release:

1. Ensure all features are merged to `develop`
2. Test thoroughly on `develop` branch
3. Update version numbers in `.csproj` files
4. Create PR from `develop` to `master`
5. Merge PR → Automatically publishes to NuGet

### For Pre-release:

1. Simply push to `develop` branch
2. Changed packages are automatically published with `-develop.BUILD_NUMBER` suffix

## Version Management

### Stable Versions
Set in each project's `.csproj` file:
```xml
<Version>1.0.0</Version>
```

### Pre-release Versions
Automatically generated:
- Format: `{Version}-develop.{BuildNumber}`
- Example: `1.0.0-develop.123`

## Manual Deployment

Use the provided scripts for manual deployment:
- `publish-packages.ps1` (PowerShell)
- `publish-packages.sh` (Bash)
- `publish-packages.bat` (Windows Batch)

See [PUBLISHING.md](../PUBLISHING.md) for details.

## Workflow Comparison

| Feature | Release Workflow | Pre-release Workflow | Legacy Workflow |
|---------|-----------------|---------------------|-----------------|
| Branches | master | develop | master, develop |
| Version | As specified | Auto-suffixed | As specified |
| Publishes | All packages | Changed only | Changed only |
| Tests | Required | Required | Not enforced |

## GitHub Secrets Required

- `NUGET_API_KEY`: Your NuGet.org API key

## Monitoring Deployments

Check workflow runs at:
- [Actions](https://github.com/roku674/MandalaConsulting/actions)
- [NuGet Packages](https://www.nuget.org/profiles/roku674)