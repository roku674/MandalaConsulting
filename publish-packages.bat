@echo off
setlocal enabledelayedexpansion

echo ========================================
echo MandalaConsulting NuGet Package Publisher
echo ========================================
echo.

:: Check if NuGet API key is provided as argument
if "%1"=="" (
    echo Error: Please provide NuGet API key as first argument
    echo Usage: publish-packages.bat YOUR_NUGET_API_KEY [version-increment]
    echo.
    echo version-increment options:
    echo   major - Increment major version (1.0.0 to 2.0.0)
    echo   minor - Increment minor version (1.0.0 to 1.1.0)
    echo   patch - Increment patch version (1.0.0 to 1.0.1) [default]
    exit /b 1
)

set NUGET_API_KEY=%1
set VERSION_INCREMENT=%2
if "%VERSION_INCREMENT%"=="" set VERSION_INCREMENT=patch

:: Clean previous builds
echo Cleaning previous builds...
dotnet clean --configuration Release
if errorlevel 1 goto :error

:: Restore dependencies
echo.
echo Restoring dependencies...
dotnet restore
if errorlevel 1 goto :error

:: Build solution
echo.
echo Building solution in Release mode...
dotnet build --configuration Release --no-restore
if errorlevel 1 goto :error

:: Run tests
echo.
echo Running all tests...
dotnet test --configuration Release --no-build --verbosity minimal
if errorlevel 1 (
    echo.
    echo ERROR: Tests failed! Fix failing tests before publishing.
    exit /b 1
)

:: Pack and publish each package
echo.
echo Packing and publishing NuGet packages...
echo.

:: List of projects to publish
set "projects=MC.Logging MC.Memory MC.APIMiddlewares MC.Repository.Mongo MC.Objects MC.Objects.API"

for %%p in (%projects%) do (
    echo ----------------------------------------
    echo Processing %%p...
    
    :: Pack the project
    echo Packing %%p...
    dotnet pack "%%p\MandalaConsulting.%%p:~3%%.csproj" --configuration Release --no-build --output nupkgs
    if errorlevel 1 (
        echo ERROR: Failed to pack %%p
        goto :error
    )
    
    :: Find the generated package
    for /f "tokens=*" %%f in ('dir /b nupkgs\MandalaConsulting.%%p:~3%%.*.nupkg 2^>nul') do (
        echo Publishing %%f to NuGet.org...
        dotnet nuget push "nupkgs\%%f" --api-key %NUGET_API_KEY% --source https://api.nuget.org/v3/index.json --skip-duplicate
        if errorlevel 1 (
            echo WARNING: Failed to publish %%f (might already exist)
        ) else (
            echo Successfully published %%f
        )
    )
)

echo.
echo ========================================
echo Package publishing completed!
echo ========================================
echo.
echo Next steps:
echo 1. Commit your changes
echo 2. Create a git tag for this release
echo 3. Push to GitHub
echo.

:: Clean up nupkgs directory
if exist nupkgs rmdir /s /q nupkgs

exit /b 0

:error
echo.
echo ERROR: Build or packaging failed!
exit /b 1