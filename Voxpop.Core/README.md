# Voxpop.Core

A .NET-based service API with PostgreSQL database support.

## Prerequisites

- [Docker](https://www.docker.com/get-started)
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- (Optional) Entity Framework Core CLI tools

## Running locally

Run the following commands inside the root folder.

### 1. Start dependencies (PostgreSQL database)

Note: This will also start the containerized application on port 5001

```bash
docker compose up -d
```

### 2. Run the application locally

```bash
dotnet run --project Voxpop.Core\src\Voxpop.Core.Api
```

or via IDE of your choice using `Voxpop.Core.Api` as the startup project.

Note: `launchSettings.json` is configured to run the application on port `4001`

### 3. Access the Swagger UI

Swagger URL: http://localhost:4001/swagger

## Database Migration

### 1. Make sure you have EF Core CLI tools installed.

```bash
dotnet tool install --global dotnet-ef
```

### 2. Create Migration
```bash
dotnet ef migrations add <name> --project .\Voxpop.Core\src\Voxpop.Core.Infrastructure --startup-project .\Voxpop.Core\src\Voxpop.Core.Api
```

### 3. (Optional) Update Database Schema

Note: Migration runs at application startup

```bash
dotnet ef database update --project .\Voxpop.Core\src\Voxpop.Core.Infrastructure --startup-project .\Voxpop.Core\src\Voxpop.Core.Api
```

## Testing Dockerfile

To test if the Dockerfile builds correctly:

```bash
docker build -f .\Voxpop.Core\src\Voxpop.Core.Api\Dockerfile -t voxpop-core-api .
```