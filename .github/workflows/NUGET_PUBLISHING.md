# NuGet Package Publishing

## Overview

This repository uses GitHub Actions to automatically publish NuGet packages when version numbers are incremented in the .csproj files.

## How It Works

1. **Version Change Detection**: The workflow compares the version in each .csproj file against the latest version published on NuGet.org
2. **Selective Publishing**: Only packages with version changes are built and published
3. **Automatic Triggers**:
   - On push to `master` branch - publishes stable releases
   - On push to `develop` branch - publishes pre-release versions with `-develop.BUILD_NUMBER` suffix
   - On GitHub release creation

## Package Version Management

To publish a new version of a package:

1. Update the `<Version>` tag in the appropriate .csproj file:
   ```xml
   <Version>0.0.10</Version>  <!-- Increment from previous version -->
   ```

2. Commit and push the change:
   ```bash
   git add MC.APIMiddlewares/MandalaConsulting.APIMiddlewares.csproj
   git commit -m "Bump APIMiddlewares version to 0.0.10"
   git push
   ```

3. The GitHub Action will automatically:
   - Detect the version change
   - Build and test the package
   - Publish to NuGet.org

## Troubleshooting

### "Package already exists" Error
This occurs when trying to publish a version that already exists on NuGet.org. The workflow now checks for this automatically and skips unchanged packages.

### API Key Issues
If you see "403 Forbidden" or "The specified API key is invalid":

1. Ensure the `NUGET_API_KEY` secret is set correctly in GitHub repository settings
2. Verify the API key has permissions for all packages:
   - MandalaConsulting.APIMiddlewares
   - MandalaConsulting.Logging
   - MandalaConsulting.Memory
   - MandalaConsulting.Objects
   - MandalaConsulting.Objects.API
   - MandalaConsulting.Repository.Mongo

3. Generate a new API key on NuGet.org if needed:
   - Go to https://www.nuget.org/account/apikeys
   - Create a new key with "Push" permissions
   - Select all your packages or use glob pattern `MandalaConsulting.*`

## Manual Publishing

To manually publish a specific package:

```bash
# Pack the project
dotnet pack MC.Objects/MandalaConsulting.Objects.csproj -c Release -o ./nupkgs

# Push to NuGet
dotnet nuget push ./nupkgs/MandalaConsulting.Objects.0.0.10.nupkg \
  --api-key YOUR_API_KEY \
  --source https://api.nuget.org/v3/index.json
```

## Workflow Files

- `.github/workflows/nuget-publish-release.yml` - Publishes stable releases from master
- `.github/workflows/nuget-publish-prerelease.yml` - Publishes pre-releases from develop
- `.github/workflows/nuget-publish.yml` - Legacy workflow (being phased out)