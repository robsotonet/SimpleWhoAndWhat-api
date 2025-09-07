WhoAndWhat API - Project Guide
Version: 1.0
Last Updated: September 7, 2025

This document serves as the central guide for developing and maintaining the WhoAndWhat backend API. It outlines the project's architecture, technology stack, coding standards, and local development setup.

1. Project Overview
The WhoAndWhat API is the backend service that powers the entire WhoAndWhat ecosystem, including the web and mobile applications. It is responsible for all business logic, data persistence, and security.

1.1. Guiding Principles
Clean Architecture: We separate concerns into distinct layers (API, Core, Infrastructure) to promote maintainability and testability.

API-First Design: The OpenAPI (Swagger) specification is the source of truth for our API contract.

Test-Driven: All business logic must be accompanied by comprehensive unit tests.

Containerized: The application is designed to be run locally and deployed using Docker containers.

2. Technology Stack
Component

Technology

Details

Framework

.NET 8

The core framework for building the Web API.

Database

PostgreSQL

Our primary relational database for data persistence.

ORM

Entity Framework Core 8

Used for data access and database migrations.

API Docs

Swagger (Swashbuckle)

For generating interactive API documentation.

Logging

Serilog

For structured and configurable application logging.

Unit Testing

xUnit & Moq

The standard frameworks for writing and executing unit tests.

Containerization

Docker & Docker Compose

For creating reproducible development and production environments.

Source Control

GitHub

The repository for all source code.

3. Architecture & Project Structure
The solution follows a Clean Architecture approach to separate concerns.

/whoandwhat-api
├── src
│   ├── WhoAndWhat.Api/          # Entry point, Controllers, DTOs, Middleware
│   ├── WhoAndWhat.Core/         # Business logic, Entities, Interfaces (Repositories, Services)
│   └── WhoAndWhat.Infrastructure/ # EF Core, Repositories Impl, DB Context
├── tests
│   ├── WhoAndWhat.Core.Tests/   # Unit tests for business logic
│   └── WhoAndWhat.Api.Tests/    # Integration tests for API endpoints
├── .github/                     # GitHub Actions workflows (CI/CD)
├── .gitignore
├── Dockerfile                   # Defines the API's docker image
├── docker-compose.yml           # For orchestrating local dev environment
└── gemini.md                    # This file

WhoAndWhat.Core: The center of the application. It contains all business entities, logic, and abstractions (interfaces). It has no dependencies on other layers.

WhoAndWhat.Infrastructure: Contains implementations for interfaces defined in Core. This includes the EF Core DbContext, repository implementations, and any external service clients (e.g., email service).

WhoAndWhat.Api: The presentation layer. It handles HTTP requests, routes them to application services, and returns responses. It depends on both Core and Infrastructure.

3.1. Repository Pattern
Data access is abstracted using the Repository Pattern. Interfaces are defined in WhoAndWhat.Core (e.g., ITaskRepository) and implemented in WhoAndWhat.Infrastructure (e.g., TaskRepository.cs). This decouples our business logic from the specific data access technology (EF Core).

4. Local Development Setup
Follow these steps to get the API running on your local machine.

4.1. Prerequisites
.NET 8 SDK

Docker Desktop

A Git client

4.2. Running with Docker (Recommended)
This is the easiest and most consistent way to run the application and its database.

Clone the repository:

git clone [https://github.com/your-org/whoandwhat-api.git](https://github.com/your-org/whoandwhat-api.git)
cd whoandwhat-api

Configure Environment:
Copy the appsettings.Development.template.json to src/WhoAndWhat.Api/appsettings.Development.json and fill in any required secrets (e.g., JWT key). The default database connection string is already configured for Docker Compose.

Build and Run:
From the root directory, run the following command:

docker-compose up --build

This will:

Pull the official Postgres image.

Build a Docker image for the .NET API.

Start containers for both the API and the database.

Apply Database Migrations:
Once the containers are running, open a new terminal and run:

dotnet ef database update --project src/WhoAndWhat.Infrastructure

4.3. Accessing the API
API Base URL: http://localhost:8080

Swagger UI: http://localhost:8080/swagger

5. Database Management
Database schema changes are managed via Entity Framework Core Migrations.

To add a new migration:

dotnet ef migrations add InitialCreate --project src/WhoAndWhat.Infrastructure --startup-project src/WhoAndWhat.Api

To apply migrations:

dotnet ef database update --project src/WhoAndWhat.Infrastructure --startup-project src/WhoAndWhat.Api

6. Logging
We use Serilog for structured logging.

Development: Logs are written to the console in a human-readable format.

Production: Configuration will be set to write logs to a file or a cloud logging service (e.g., Azure App Insights, Seq).

All logs should be structured with relevant context (e.g., UserId, RequestId).

7. Unit Testing
All new business logic and complex controller logic must be covered by unit tests.

Test Project: tests/WhoAndWhat.Core.Tests

Frameworks: xUnit for the test runner, Moq for creating mock dependencies.

To run tests:

dotnet test

8. Source Control & Branching
Repository: The official code repository is located on GitHub.

Branching Strategy: We follow a simplified GitFlow model.

main: Represents the latest production-ready code. Merges only happen from develop.

develop: The main development branch. All feature branches are created from here.

feature/...: Branches for new features (e.g., feature/calendar-view).

Pull Requests: All merges into develop and main must be done through a Pull Request (PR) that requires at least one approval and for all automated checks (builds, tests) to pass.