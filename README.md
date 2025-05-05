# Mandala Consulting .NET Code Repository

Welcome to the Mandala Consulting repository! This repository contains our open-sourced .NET code, aimed at providing useful libraries and tools for the community.

## Table of Contents

- [About Mandala Consulting](#about-mandala-consulting)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [NuGet Packages](#nuget-packages)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## About Mandala Consulting

Mandala Consulting is dedicated to delivering high-quality software solutions and fostering innovation within the .NET community. By sharing our code, we hope to empower developers and promote collaboration.

## Features

- Comprehensive libraries for various .NET applications.
- Well-documented code with examples and usage guidelines.
- Continuous integration and testing to ensure code quality.

## Installation

To get started with our libraries, you can add them to your .NET project using NuGet. Use the following command in your terminal:

```bash
dotnet add package <PackageName>
```

## NuGet Packages

Our libraries are available as NuGet packages. Here are the packages currently available:

- **MandalaConsulting.APIMiddlewares** - Middleware components for ASP.NET Core APIs
- **MandalaConsulting.Logging** - Standardized logging utilities
- **MandalaConsulting.Memory** - Memory management utilities
- **MandalaConsulting.Objects** - Common object models
- **MandalaConsulting.Objects.API** - API-specific object models
- **MandalaConsulting.Repository.Mongo** - MongoDB repository pattern implementation

### Automatic Package Publishing

Our packages are automatically published to NuGet.org when version numbers are updated and pushed to the repository. The publishing workflow:

1. Automatically detects changes in .csproj files
2. Extracts the version number from each modified project file
3. Builds the projects in Release mode
4. Publishes updated packages to NuGet.org

Manual publishing is also available using the script in `scripts/publish-nuget-packages.sh`.
