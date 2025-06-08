# NuGet Package Publishing

## Overview

This repository uses GitHub Actions to automatically publish NuGet packages when version numbers are incremented in the .csproj files.

## How It Works

1. **Version Change Detection**: The workflow compares the version in each .csproj file against the latest version published on NuGet.org
2. **Selective Publishing**: Only packages with version changes are built and published
3. **Automatic Trigger**: Publishing occurs when changes are pushed to the `master` branch

## Package Version Management

To publish a new version of a package:

1. Update the `<Version>` tag in the appropriate .csproj file:
   ```xml
   <Version>0.0.10</Version>  <!-- Increment from previous version -->
   ```

2. Create a pull request to merge your changes into master:
   ```bash
   git checkout -b feature/update-package-version
   git add MC.APIMiddlewares/MandalaConsulting.APIMiddlewares.csproj
   git commit -m "Bump APIMiddlewares version to 0.0.10"
   git push origin feature/update-package-version
   ```

3. After the PR is merged to master, the GitHub Action will automatically:
   - Detect the version change
   - Build and test the package
   - Publish to NuGet.org

## Important Notes

- **No automatic publishing from develop branch**: Packages are only published when merged to master
- **Version must be incremented**: The workflow will skip packages where the version hasn't changed
- **All tests must pass**: Publishing will fail if any tests fail

## Troubleshooting

### "Package already exists" Error
This occurs when trying to publish a version that already exists on NuGet.org. The workflow automatically checks for this and skips unchanged packages.

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

Or use the provided script:
```bash
export NUGET_API_KEY=your_api_key
./publish-packages.sh
```

## Workflow File

- `.github/workflows/nuget-publish.yml` - Main workflow that publishes packages from master branch only