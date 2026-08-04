# ASP.NET Core 10 Web API Starter

![.NET](https://img.shields.io/badge/.NET-10-blue)
![License](https://img.shields.io/badge/license-MIT-green)
![Docker](https://img.shields.io/badge/docker-ready-blue)

> A starter template for building scalable ASP.NET Core 10 Web APIs using Clean Architecture, JWT authentication, role & permission-based authorization, Entity Framework Core, and PostgreSQL.

## Features

### Architecture

- ✅ Clean Architecture
- ✅ Repository Pattern
- ✅ Service Layer

### Security

- ✅ JWT Authentication
- ✅ Role & Permission-based Authorization

### Persistence

- ✅ Entity Framework Core
- ✅ PostgreSQL

### Developer Experience

- ✅ Docker & Docker Compose
- ✅ Swagger / OpenAPI
- ✅ Global Exception Handling
- ✅ Dependency Injection
- ✅ Configuration via Options Pattern

## Project Structure

```text
src/
├── Application/
│   ├── Interfaces/
│   ├── Models/
│   └── Services/
│
├── Core/
│   ├── Entities/
│   ├── Enums/
│   ├── Exceptions/
│   ├── Interfaces/
│   └── Structs/
│
├── DataAccess/
│   ├── Configurations/
│   ├── DbContexts/
│   ├── Repositories/
│   └── AuthorizationOptions.cs
│
├── Infrastructure/
│   ├── Auth/
│   └── Mapping/
│
└── Server/
    ├── Controllers/
    ├── Extensions/
    ├── Filters/
    └── Program.cs
```

## Architecture

The project follows **Clean Architecture** principles.

### Core

Contains:

* Entities
* Enums
* Exceptions
* Domain interfaces

No dependencies.

### Application

Contains:

* Service interfaces
* Business logic
* Models

Depends only on **Core**.

### DataAccess

Contains:

* EF Core
* DbContext
* Repository implementations
* Migrations

Depends only on **Core**.

### Infrastructure

Contains:

* JWT
* Authorization
* Password hashing
* External services

Depends on Application and Core.

### Server

Contains:

* Controllers
* Dependency Injection
* Middleware
* Swagger
* Authentication configuration

## Authentication

JWT Bearer authentication.

Authentication flow:

```
Client
    │
    ▼
POST /auth/login
    │
    ▼
Receive JWT Token
    │
    ▼
Authorization: Bearer <token>
    │
    ▼
Protected Endpoints
```

## Authorization

Supports:

* Roles
* Permissions
* Custom Authorization Policy Provider

Example:

```csharp
[Authorize]
```

```csharp
[HasPermission(Permission.CreatePosts)]
```

## Technologies

* ASP.NET Core 10
* Entity Framework Core 10
* PostgreSQL
* Docker
* Docker Compose
* JWT
* Swagger

## Running

```bash
git clone https://github.com/gurori/jwt-auth-dotnet10-api.git

cd jwt-auth-dotnet10-api

docker compose -f docker-compose.yml -f docker-compose.dev.yml up --build
```

The API will be available at:

```text
http://localhost:8080
```

Swagger UI:

```text
http://localhost:8080/swagger
```

## Configuration

Configure the application using [appsettings.json](Server/appsettings.json) and [appsettings.Development.json](Server/appsettings.Development.json) for development.

> [!NOTE]
> Replace all placeholder values (such as `your-frontend.com` and `very-secret-key-...`) with your own configuration before running the application.

Example:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "AppDbContext": "Server=db;Port=5432;Database=app;Username=app;Password=app_password;"
  },
  "JwtOptions": {
    "SecretKey": "very-secret-key-c6655065-4095-48df-85a1-227b4277c606-plus-hopes-and-dreams",
    "Issuer": "https://your-backend.com",
    "Audience": "https://your-frontend.com",
    "ExpiresDays": "14",
    "JwtCookieName": "auth"
  },
  "Cors": {
    "Origins": [
      "https://your-frontend.com"
    ]
  },
  "AuthorizationOptions": {
    "RolePermissions": [
      {
        "Role": "User",
        "Permissions": [ "GetUsers" ]
      },
      {
        "Role": "Admin",
        "Permissions": [ "GetUsers", "CreatePosts", "UpdatePosts", "DeletePosts" ]
      }
    ]
  }
}
```

> [!WARNING]
> Never use the example JWT secret or development configuration in production. Always use strong secrets and environment-specific settings.

## Docker

The project includes two Docker Compose files:

- **docker-compose.yml** – base configuration for running the application and its dependencies.
- **docker-compose.dev.yml** – development overrides (for example, source code mounting, development environment variables, and hot reload).

Run both files together during development:

```bash
docker compose -f docker-compose.yml -f docker-compose.dev.yml up --build
```

> [!TIP]
> Review both Docker Compose files and update service names, ports, environment variables, and volume mappings to match your local environment before starting the application.
```

## Goals

This project is intended as a starting point for building production-ready ASP.NET Core Web APIs.

It demonstrates:

* Clean Architecture
* Dependency Injection
* Authentication
* Authorization
* Entity Framework Core
* Dockerized development
* Maintainable project structure

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
