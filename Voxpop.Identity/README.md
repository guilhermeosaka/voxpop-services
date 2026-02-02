# Voxpop.Identity

A .NET-based service API with PostgreSQL database support.

## Prerequisites

- [Docker](https://www.docker.com/get-started)
- [.NET SDK](https://dotnet.microsoft.com/download)
- Entity Framework Core CLI tools

## Getting Started

Run the following commands inside the root folder.

### 1. Install Entity Framework Core Tools

```bash
dotnet tool install --global dotnet-ef
```

### 2. Start PostgreSQL Database

```bash
docker compose up -d
```

### 3. Update Database Schema

```bash
dotnet ef database update --project .\Voxpop.Identity\src\Voxpop.Identity.Infrastructure --startup-project .\Voxpop.Identity\src\Voxpop.Identity.Api
```

## Database Migration

### 1. Create Migration
```bash
dotnet ef migrations add Initial --project .\Voxpop.Identity\src\Voxpop.Identity.Infrastructure --startup-project .\Voxpop.Identity\src\Voxpop.Identity.Api
```

### 2. Update Database Schema

```bash
dotnet ef database update --project .\Voxpop.Identity\src\Voxpop.Identity.Infrastructure --startup-project .\Voxpop.Identity\src\Voxpop.Identity.Api
```

## Testing Dockerfile

To test if the Dockerfile builds correctly:

```bash
docker build -f .\Voxpop.Identity\src\Voxpop.Identity.Api\Dockerfile -t voxpop-identity .
```