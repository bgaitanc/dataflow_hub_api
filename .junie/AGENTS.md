# DataFlow Hub - Project Overview

DataFlow Hub is a comprehensive academic management system designed to handle students, teachers, courses, enrollments, grades, and reports. It follows a Clean Architecture approach using .NET 10 and C# 14.

## Core Features

- **Students & Teachers Management**: Handling profile data and academic history.
- **Academic Administration**: Managing courses, class groups, and schedules.
- **Enrollment System**: Student registration into courses with status tracking.
- **Grade Tracking**: Managing student performance and reports.
- **Identity & Auth**: User authentication and authorization using JWT.

## Project Structure

The solution is divided into the following layers:

- **DataFlowHub.Api**: The entry point of the application. It contains the controllers, API configurations, and handles the HTTP request/response flow.
- **DataFlowHub.Application**: Contains the business logic, services, and interfaces. It uses **DataFlowHub.Application.Models** for Data Transfer Objects (DTOs).
- **DataFlowHub.Domain**: The core of the system. It contains the domain entities (Academic, Actors, Identity), custom exceptions, and repository interfaces.
- **DataFlowHub.Infrastructure**: Handles the data access layer, EF Core database configurations, migrations, and concrete repository implementations.
- **DataFlowHub.Tests**: Contains unit and integration tests for the various layers.

## Technology Stack

- **Framework**: .NET 10.0 (ASP.NET Core)
- **Language**: C# 15.0
- **Database**: SQL Server (configured via EF Core)
- **Authentication**: JWT Bearer + ASP.NET Core Identity
- **Architecture**: Clean Architecture / Domain-Driven Design (DDD) principles.

## Development Guidelines

### 1. Code Style and Standards
- Follow standard C# 15 and .NET naming conventions (PascalCase for classes/methods, camelCase for local variables).
- Maintain consistency with the existing codebase patterns.
- Ensure that any new domain entities or database changes are followed by creating an EF Core migration in `DataFlowHub.Infrastructure`.

### 2. Testing
- Junie should run tests to check the correctness of the proposed solution when changes affect business logic or data access.
- Tests can be run using the `run_test` tool, targeting `DataFlowHub.UnitTest.csproj` or specific namespaces.
- Before submitting, ensure that all relevant tests in `DataFlowHub.UnitTest` pass.

### 3. Building the Project
- Junie should verify that the project builds after making changes, especially if they involve API signatures or dependency injection updates in `Program.cs`.

### 4. Database Migrations
- When modifying entities in `DataFlowHub.Domain.Entities`, remember to add a migration in the `DataFlowHub.Infrastructure` project:
  `dotnet ef migrations add <MigrationName> --project DataFlowHub.Infrastructure --startup-project DataFlowHub.Api`

### 5. Git Commits
- Only commit when explicitly requested.
- Use descriptive commit messages and always include Junie as a co-author: `--trailer "Co-authored-by: Junie <junie@jetbrains.com>"`

### 6. API Client Testing (DataFlowHub.Api.http)
- Whenever a new controller or endpoint is added, update the `DataFlowHub.Api\DataFlowHub.Api.http` file with its corresponding request example.

### 7. API Controllers and Service Execution
- All new controllers must inherit from `DataFlowHubControllerBase` (located in `DataFlowHub.Api.Controllers.Base`).
- Use the `ExecuteServiceAsync` method provided by `DataFlowHubControllerBase` to wrap service calls. This ensures a standardized response format using `DataFlowHubGenericResponse` or `DataFlowHubGenericResponse<T>`.

### 8. Pagination and Filtering
- All GET endpoints for collections MUST support pagination and filtering.
- Use `BaseFilterDto` as a base for filter DTOs.
- Repositories should implement a `GetPagedAsync` method that returns a tuple `(IEnumerable<T> Items, int TotalCount)`.
- Services should return `PagedResultDto<T>`.
