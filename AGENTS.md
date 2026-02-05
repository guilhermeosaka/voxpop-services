# AGENTS.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Build & Run Commands

```powershell
# Build entire solution
dotnet build Voxpop.sln

# Build a specific project
dotnet build Voxpop.Identity/src/Voxpop.Identity.Api/Voxpop.Identity.Api.csproj

# Run with Docker Compose (starts all services including PostgreSQL, RabbitMQ, Seq)
docker compose up -d

# Run individual service (without Docker)
dotnet run --project Voxpop.Identity/src/Voxpop.Identity.Api
dotnet run --project Voxpop.Core/src/Voxpop.Core.Api
```

## Database Migrations (EF Core)

Each service has its own database. Commands must be run from the solution root.

```powershell
# Identity service migrations
dotnet ef migrations add <MigrationName> --project .\Voxpop.Identity\src\Voxpop.Identity.Infrastructure --startup-project .\Voxpop.Identity\src\Voxpop.Identity.Api
dotnet ef database update --project .\Voxpop.Identity\src\Voxpop.Identity.Infrastructure --startup-project .\Voxpop.Identity\src\Voxpop.Identity.Api

# Core service migrations
dotnet ef migrations add <MigrationName> --project .\Voxpop.Core\src\Voxpop.Core.Infrastructure --startup-project .\Voxpop.Core\src\Voxpop.Core.Api
dotnet ef database update --project .\Voxpop.Core\src\Voxpop.Core.Infrastructure --startup-project .\Voxpop.Core\src\Voxpop.Core.Api
```

## Architecture Overview

This is a microservices solution using **Clean Architecture** with a custom **CQRS** pattern.

### Services
- **Voxpop.Core** - Main business logic (polls, profiles) on port 5001
- **Voxpop.Identity** - Authentication (JWT, OTP, phone verification) on port 5002
- **Voxpop.Notification** - Background worker for SMS/email delivery via RabbitMQ
- **Voxpop.Template** - Starter template for new services

### Clean Architecture Layers (per service)
Each service (e.g., `Voxpop.Identity`) follows this structure:
- **Api** - Controllers, DTOs, middleware. Controllers use `IDispatcher` to dispatch commands/queries.
- **Application** - Business logic in `Handlers/` folder organized by feature. Contains commands, queries, and their handlers. Defines interfaces for infrastructure.
- **Domain** - Entities in `Models/`, repository interfaces in `Interfaces/`, enums.
- **Infrastructure** - EF Core DbContext, repository implementations, external services (Twilio, RabbitMQ).

### CQRS Pattern (`Voxpop.Packages.Dispatcher`)
Commands/Queries are handled via a dispatcher pattern:
- `IHandler<TRequest>` - Returns `Result` (no data)
- `IHandler<TRequest, TResult>` - Returns `Result<TResult>`
- Controllers call `dispatcher.Dispatch<TCommand, TResult>(command, ct)`
- Handlers live in `Application/Handlers/{Feature}/{UseCase}/`

Example handler structure:
```
Handlers/
  Tokens/
    CreateToken/
      CreateTokenCommand.cs    # The request record
      CreateTokenHandler.cs    # The handler implementing IHandler<,>
      CreateTokenResult.cs     # The result type
```

### Result Pattern (`Voxpop.Packages.Dispatcher.Types`)
All handlers return `Result` or `Result<T>`:
- `Result.Success()` for void success
- Implicit conversion from `TResult` to `Result<TResult>` for success with value
- Implicit conversion from `Error` to `Result` for failures
- Errors are converted to HTTP responses via `ResultExtensions.ToActionResult()`

### Shared Packages (`Voxpop.Packages`)
- **Contracts** - Cross-service event contracts (e.g., `SmsSend`, `EmailCodeSend`)
- **Dispatcher** - CQRS dispatcher, `Result<T>`, `Error` types
- **Extensions** - Serilog configuration, common service extensions

### Inter-Service Communication
Services communicate via RabbitMQ using MassTransit:
- Identity publishes `SmsSend` events
- Notification consumes and sends via Twilio

## Infrastructure
- **PostgreSQL** - Separate database per service (`identity_db`, `core_db`)
- **RabbitMQ** - Message broker at port 5672 (management UI at 15672)
- **Seq** - Log aggregation at port 5341
- **AWS ECS** - Production deployment via Terraform in `infra/`

## Adding a New Feature

1. Create command/query record in `Application/Handlers/{Feature}/{UseCase}/`
2. Create handler implementing `IHandler<TCommand, TResult>`
3. Register handler via `ServiceCollectionExtensions.AddApplicationServices()`
4. Add controller endpoint that dispatches the command
5. Add any new repository methods to Domain interfaces, implement in Infrastructure
