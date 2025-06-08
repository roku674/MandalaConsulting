#!/bin/bash

# This script checks for version changes in .csproj files and publishes
# updated packages to NuGet.org

# Ensure NUGET_API_KEY is set
if [ -z "$NUGET_API_KEY" ]; then
  echo "Error: NUGET_API_KEY environment variable is not set."
  echo "Please set it using: export NUGET_API_KEY=your_nuget_api_key"
  exit 1
fi

# Get list of all project files, excluding test projects
PROJECT_FILES=$(find . -name "*.csproj" | grep -v "Tests.csproj" | grep -v "Test.csproj")

# Build the solution in Release mode
echo "Building solution in Release mode..."
dotnet build --configuration Release

# Process each project
for PROJECT_FILE in $PROJECT_FILES; do
  # Skip test projects
  if [[ $PROJECT_FILE == *"Test"* ]]; then
    continue
  fi
  
  # Extract version
  VERSION=$(grep -o '<Version>[^<]*</Version>' "$PROJECT_FILE" | sed 's/<Version>\(.*\)<\/Version>/\1/')
  
  if [ -n "$VERSION" ]; then
    PROJECT_DIR=$(dirname "$PROJECT_FILE")
    PROJECT_NAME=$(basename "$PROJECT_FILE" .csproj)
    
    echo "Processing $PROJECT_NAME (Version: $VERSION)"
    
    # Find NuGet package in project output
    NUPKG_PATH=$(find "$PROJECT_DIR/bin/Release" -name "$PROJECT_NAME.$VERSION.nupkg" -type f)
    
    if [ -n "$NUPKG_PATH" ]; then
      echo "Publishing $NUPKG_PATH to NuGet.org"
      dotnet nuget push "$NUPKG_PATH" --api-key "$NUGET_API_KEY" --source https://api.nuget.org/v3/index.json --skip-duplicate
    else
      echo "No NuGet package found for $PROJECT_NAME v$VERSION"
    fi
  else
    echo "No version found in $PROJECT_FILE"
  fi
done

echo "Publishing process completed!"