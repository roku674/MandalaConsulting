param(
    [Parameter(Mandatory=$true)]
    [string]$NuGetApiKey,
    
    [Parameter(Mandatory=$false)]
    [ValidateSet("major", "minor", "patch")]
    [string]$VersionIncrement = "patch",
    
    [Parameter(Mandatory=$false)]
    [switch]$SkipTests = $false,
    
    [Parameter(Mandatory=$false)]
    [switch]$DryRun = $false
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "MandalaConsulting NuGet Package Publisher" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Function to increment version
function Update-Version {
    param(
        [string]$CurrentVersion,
        [string]$Increment
    )
    
    $parts = $CurrentVersion.Split('.')
    $major = [int]$parts[0]
    $minor = [int]$parts[1]
    $patch = [int]$parts[2]
    
    switch ($Increment) {
        "major" { 
            $major++
            $minor = 0
            $patch = 0
        }
        "minor" { 
            $minor++
            $patch = 0
        }
        "patch" { 
            $patch++
        }
    }
    
    return "$major.$minor.$patch"
}

# Function to update version in csproj file
function Update-ProjectVersion {
    param(
        [string]$ProjectPath,
        [string]$NewVersion
    )
    
    $content = Get-Content $ProjectPath -Raw
    $content = $content -replace '<Version>[\d\.]+</Version>', "<Version>$NewVersion</Version>"
    
    if (-not $DryRun) {
        Set-Content -Path $ProjectPath -Value $content -NoNewline
        Write-Host "Updated $ProjectPath to version $NewVersion" -ForegroundColor Green
    } else {
        Write-Host "DRY RUN: Would update $ProjectPath to version $NewVersion" -ForegroundColor Yellow
    }
}

# Define projects
$projects = @(
    @{ Name = "Logging"; Path = "MC.Logging\MandalaConsulting.Logging.csproj" },
    @{ Name = "Memory"; Path = "MC.Memory\MandalaConsulting.Memory.csproj" },
    @{ Name = "APIMiddlewares"; Path = "MC.APIMiddlewares\MandalaConsulting.APIMiddlewares.csproj" },
    @{ Name = "Repository.Mongo"; Path = "MC.Repository.Mongo\MandalaConsulting.Repository.Mongo.csproj" },
    @{ Name = "Objects"; Path = "MC.Objects\MandalaConsulting.Objects.csproj" },
    @{ Name = "Objects.API"; Path = "MC.Objects.API\MandalaConsulting.Objects.API.csproj" }
)

# Update versions if not dry run
if ($VersionIncrement -and -not $DryRun) {
    Write-Host "Updating package versions..." -ForegroundColor Yellow
    foreach ($project in $projects) {
        if (Test-Path $project.Path) {
            $content = Get-Content $project.Path -Raw
            if ($content -match '<Version>([\d\.]+)</Version>') {
                $currentVersion = $matches[1]
                $newVersion = Update-Version -CurrentVersion $currentVersion -Increment $VersionIncrement
                Update-ProjectVersion -ProjectPath $project.Path -NewVersion $newVersion
            }
        }
    }
    Write-Host ""
}

# Clean previous builds
Write-Host "Cleaning previous builds..." -ForegroundColor Yellow
dotnet clean --configuration Release | Out-Null
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Failed to clean solution" -ForegroundColor Red
    exit 1
}

# Restore dependencies
Write-Host "Restoring dependencies..." -ForegroundColor Yellow
dotnet restore | Out-Null
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Failed to restore dependencies" -ForegroundColor Red
    exit 1
}

# Build solution
Write-Host "Building solution in Release mode..." -ForegroundColor Yellow
dotnet build --configuration Release --no-restore | Out-Null
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Build failed" -ForegroundColor Red
    exit 1
}

# Run tests unless skipped
if (-not $SkipTests) {
    Write-Host "Running all tests..." -ForegroundColor Yellow
    dotnet test --configuration Release --no-build --verbosity minimal
    if ($LASTEXITCODE -ne 0) {
        Write-Host "ERROR: Tests failed! Fix failing tests before publishing." -ForegroundColor Red
        exit 1
    }
} else {
    Write-Host "Skipping tests (not recommended for production releases)" -ForegroundColor Yellow
}

# Create nupkgs directory
if (-not (Test-Path "nupkgs")) {
    New-Item -ItemType Directory -Path "nupkgs" | Out-Null
}

# Pack and publish each package
Write-Host ""
Write-Host "Packing and publishing NuGet packages..." -ForegroundColor Yellow
Write-Host ""

$successCount = 0
$failCount = 0

foreach ($project in $projects) {
    Write-Host "----------------------------------------" -ForegroundColor Gray
    Write-Host "Processing $($project.Name)..." -ForegroundColor Cyan
    
    if (Test-Path $project.Path) {
        # Get current version
        $content = Get-Content $project.Path -Raw
        if ($content -match '<Version>([\d\.]+)</Version>') {
            $version = $matches[1]
            Write-Host "Version: $version" -ForegroundColor Gray
        }
        
        # Pack the project
        Write-Host "Packing $($project.Name)..." -ForegroundColor Yellow
        dotnet pack $project.Path --configuration Release --no-build --output nupkgs | Out-Null
        
        if ($LASTEXITCODE -ne 0) {
            Write-Host "ERROR: Failed to pack $($project.Name)" -ForegroundColor Red
            $failCount++
            continue
        }
        
        # Find and publish the package
        $packageFiles = Get-ChildItem -Path "nupkgs" -Filter "MandalaConsulting.$($project.Name).*.nupkg" | Sort-Object -Descending
        
        if ($packageFiles) {
            $packageFile = $packageFiles[0]
            
            if (-not $DryRun) {
                Write-Host "Publishing $($packageFile.Name) to NuGet.org..." -ForegroundColor Yellow
                dotnet nuget push $packageFile.FullName --api-key $NuGetApiKey --source https://api.nuget.org/v3/index.json --skip-duplicate
                
                if ($LASTEXITCODE -eq 0) {
                    Write-Host "Successfully published $($packageFile.Name)" -ForegroundColor Green
                    $successCount++
                } else {
                    Write-Host "WARNING: Failed to publish $($packageFile.Name) (might already exist)" -ForegroundColor Yellow
                    $failCount++
                }
            } else {
                Write-Host "DRY RUN: Would publish $($packageFile.Name)" -ForegroundColor Yellow
                $successCount++
            }
        } else {
            Write-Host "ERROR: No package file found for $($project.Name)" -ForegroundColor Red
            $failCount++
        }
    } else {
        Write-Host "ERROR: Project file not found: $($project.Path)" -ForegroundColor Red
        $failCount++
    }
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Package publishing completed!" -ForegroundColor Cyan
Write-Host "Success: $successCount, Failed: $failCount" -ForegroundColor $(if ($failCount -eq 0) { "Green" } else { "Yellow" })
Write-Host "========================================" -ForegroundColor Cyan

if (-not $DryRun -and $VersionIncrement) {
    Write-Host ""
    Write-Host "Next steps:" -ForegroundColor Yellow
    Write-Host "1. Commit your version changes: git add -A && git commit -m 'Bump versions for release'" -ForegroundColor Gray
    Write-Host "2. Create a git tag: git tag v$newVersion" -ForegroundColor Gray
    Write-Host "3. Push to GitHub: git push origin develop --tags" -ForegroundColor Gray
}

# Clean up nupkgs directory
if (Test-Path "nupkgs") {
    Remove-Item -Path "nupkgs" -Recurse -Force
}

exit $(if ($failCount -eq 0) { 0 } else { 1 })