#!/bin/bash

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

echo -e "${CYAN}========================================"
echo "MandalaConsulting NuGet Package Publisher"
echo -e "========================================${NC}"
echo

# Check if NuGet API key is provided
if [ -z "$1" ]; then
    echo -e "${RED}Error: Please provide NuGet API key as first argument${NC}"
    echo "Usage: ./publish-packages.sh YOUR_NUGET_API_KEY [version-increment]"
    echo
    echo "version-increment options:"
    echo "  major - Increment major version (1.0.0 to 2.0.0)"
    echo "  minor - Increment minor version (1.0.0 to 1.1.0)"
    echo "  patch - Increment patch version (1.0.0 to 1.0.1) [default]"
    exit 1
fi

NUGET_API_KEY=$1
VERSION_INCREMENT=${2:-patch}

# Function to update version
update_version() {
    local current_version=$1
    local increment=$2
    
    IFS='.' read -r major minor patch <<< "$current_version"
    
    case $increment in
        major)
            ((major++))
            minor=0
            patch=0
            ;;
        minor)
            ((minor++))
            patch=0
            ;;
        patch)
            ((patch++))
            ;;
    esac
    
    echo "$major.$minor.$patch"
}

# Function to update project version
update_project_version() {
    local project_path=$1
    local new_version=$2
    
    if [ -f "$project_path" ]; then
        sed -i.bak "s/<Version>[0-9.]*<\/Version>/<Version>$new_version<\/Version>/" "$project_path"
        rm "${project_path}.bak"
        echo -e "${GREEN}Updated $project_path to version $new_version${NC}"
    fi
}

# Define projects
declare -a projects=(
    "MC.Logging/MandalaConsulting.Logging.csproj"
    "MC.Memory/MandalaConsulting.Memory.csproj"
    "MC.APIMiddlewares/MandalaConsulting.APIMiddlewares.csproj"
    "MC.Repository.Mongo/MandalaConsulting.Repository.Mongo.csproj"
    "MC.Objects/MandalaConsulting.Objects.csproj"
    "MC.Objects.API/MandalaConsulting.Objects.API.csproj"
)

# Update versions if requested
if [ "$VERSION_INCREMENT" != "none" ]; then
    echo -e "${YELLOW}Updating package versions...${NC}"
    for project in "${projects[@]}"; do
        if [ -f "$project" ]; then
            current_version=$(grep -o '<Version>[^<]*</Version>' "$project" | sed 's/<Version>\(.*\)<\/Version>/\1/')
            if [ -n "$current_version" ]; then
                new_version=$(update_version "$current_version" "$VERSION_INCREMENT")
                update_project_version "$project" "$new_version"
            fi
        fi
    done
    echo
fi

# Clean previous builds
echo -e "${YELLOW}Cleaning previous builds...${NC}"
dotnet clean --configuration Release > /dev/null 2>&1
if [ $? -ne 0 ]; then
    echo -e "${RED}ERROR: Failed to clean solution${NC}"
    exit 1
fi

# Restore dependencies
echo -e "${YELLOW}Restoring dependencies...${NC}"
dotnet restore > /dev/null 2>&1
if [ $? -ne 0 ]; then
    echo -e "${RED}ERROR: Failed to restore dependencies${NC}"
    exit 1
fi

# Build solution
echo -e "${YELLOW}Building solution in Release mode...${NC}"
dotnet build --configuration Release --no-restore > /dev/null 2>&1
if [ $? -ne 0 ]; then
    echo -e "${RED}ERROR: Build failed${NC}"
    exit 1
fi

# Run tests
echo -e "${YELLOW}Running all tests...${NC}"
dotnet test --configuration Release --no-build --verbosity minimal
if [ $? -ne 0 ]; then
    echo -e "${RED}ERROR: Tests failed! Fix failing tests before publishing.${NC}"
    exit 1
fi

# Create nupkgs directory
mkdir -p nupkgs

# Pack and publish each package
echo
echo -e "${YELLOW}Packing and publishing NuGet packages...${NC}"
echo

success_count=0
fail_count=0

for project in "${projects[@]}"; do
    echo "----------------------------------------"
    project_name=$(basename "$project" .csproj | sed 's/MandalaConsulting\.//')
    echo -e "${CYAN}Processing $project_name...${NC}"
    
    if [ -f "$project" ]; then
        # Get current version
        version=$(grep -o '<Version>[^<]*</Version>' "$project" | sed 's/<Version>\(.*\)<\/Version>/\1/')
        echo -e "Version: $version"
        
        # Pack the project
        echo -e "${YELLOW}Packing $project_name...${NC}"
        dotnet pack "$project" --configuration Release --no-build --output nupkgs > /dev/null 2>&1
        
        if [ $? -ne 0 ]; then
            echo -e "${RED}ERROR: Failed to pack $project_name${NC}"
            ((fail_count++))
            continue
        fi
        
        # Find and publish the package
        package_file=$(ls nupkgs/MandalaConsulting.$project_name.*.nupkg 2>/dev/null | head -1)
        
        if [ -n "$package_file" ]; then
            echo -e "${YELLOW}Publishing $(basename "$package_file") to NuGet.org...${NC}"
            dotnet nuget push "$package_file" --api-key "$NUGET_API_KEY" --source https://api.nuget.org/v3/index.json --skip-duplicate
            
            if [ $? -eq 0 ]; then
                echo -e "${GREEN}Successfully published $(basename "$package_file")${NC}"
                ((success_count++))
            else
                echo -e "${YELLOW}WARNING: Failed to publish $(basename "$package_file") (might already exist)${NC}"
                ((fail_count++))
            fi
        else
            echo -e "${RED}ERROR: No package file found for $project_name${NC}"
            ((fail_count++))
        fi
    else
        echo -e "${RED}ERROR: Project file not found: $project${NC}"
        ((fail_count++))
    fi
done

echo
echo -e "${CYAN}========================================"
echo "Package publishing completed!"
echo -e "Success: $success_count, Failed: $fail_count"
echo -e "========================================${NC}"

if [ "$VERSION_INCREMENT" != "none" ]; then
    echo
    echo -e "${YELLOW}Next steps:${NC}"
    echo "1. Commit your version changes: git add -A && git commit -m 'Bump versions for release'"
    echo "2. Create a git tag: git tag v$new_version"
    echo "3. Push to GitHub: git push origin develop --tags"
fi

# Clean up nupkgs directory
rm -rf nupkgs

exit $( [ $fail_count -eq 0 ] && echo 0 || echo 1 )