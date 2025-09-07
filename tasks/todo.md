# WhoAndWhat API - Development Plan

This work plan is based on `gemini.md` and `project.md`. It outlines the tasks to evolve the current API to match the full project vision and divides the work between two developers.

---

## Developer 1: Architecture & Backend Foundation

This developer will focus on creating the project structure from scratch, implementing core architectural patterns, and handling authentication and user management.

### Phase 1: Create Initial Project Structure
- [ ] **Task 1.1: Create Solution and Projects:**
    - [ ] Create a new .NET solution named `WhoAndWhat.sln`.
    - [ ] Create the following projects based on the Clean Architecture model described in `project.md`:
        - `src/WhoAndWhat.Domain` (Class Library for entities, enums, aggregates)
        - `src/WhoAndWhat.Application` (Class Library for CQRS, business logic, DTOs)
        - `src/WhoAndWhat.Infrastructure` (Class Library for EF Core, DB context, repository implementations)
        - `src/WhoAndWhat.Api` (ASP.NET Core Web API project)
    - [ ] Set up project references:
        - `Api` -> `Application`, `Infrastructure`
        - `Application` -> `Domain`
        - `Infrastructure` -> `Application`
- [ ] **Task 1.2: Add Core Dependencies:**
    - [ ] Add `MediatR` to `WhoAndWhat.Application`.
    - [ ] Add `FluentValidation.AspNetCore` to `WhoAndWhat.Api`.
    - [ ] Add `Swashbuckle` (Swagger) to `WhoAndWhat.Api`.
- [ ] **Task 1.3: Basic API Setup:**
    - [ ] Configure basic startup and dependency injection in `WhoAndWhat.Api`.
    - [ ] Create a placeholder "health check" endpoint (e.g., `/health`) to verify the API runs.

### Phase 2: Core Feature Implementation
- [ ] **Task 2.1: Implement Authentication:**
    - [ ] Implement JWT generation and validation logic.
    - [ ] Create initial endpoints for `/auth/register` and `/auth/login`.
    - [ ] Add logic for refresh tokens and a `/auth/refresh` endpoint.
- [ ] **Task 2.2: User Account Management:**
    - [ ] Implement endpoints for user profile management (`/users`).
    - [ ] Define user subscription tiers (Free vs. Premium) in the `User` entity.
- [ ] **Task 2.3: Implement Tier Enforcement Middleware:**
    - [ ] Create ASP.NET Core middleware to check user subscription tier.
    - [ ] Apply middleware to endpoints/features that are for premium users only.

---

## Developer 2: Database, Features & DevOps

This developer will focus on the data model, implementing core business features, and setting up the development and deployment infrastructure.

### Phase 1: Database & Data Model
- [ ] **Task 1.1: Define Core Entities:**
    - [ ] In `WhoAndWhat.Domain`, create the initial EF Core entities for `User`, `Contact`, `Task`, `Subtask`, `ShoppingList`, `ShoppingItem`, `Message`, and `Notification`.
    - [ ] Establish relationships between entities (e.g., User -> Tasks, Task -> Subtasks).
- [ ] **Task 1.2: Set up Database Context and Migrations:**
    - [ ] Add `Microsoft.EntityFrameworkCore.PostgreSQL` to `WhoAndWhat.Infrastructure`.
    - [ ] Create the `ApplicationDbContext` in `Infrastructure`.
    - [ ] Generate the initial database migration using `dotnet ef migrations add InitialCreate`.
- [ ] **Task 1.3: Configure Serilog for Structured Logging:**
    - [ ] Add and configure Serilog in `WhoAndWhat.Api`.
    - [ ] Configure it to include `UserId` and `RequestId` in log entries.
    - [ ] Set up production logging configuration to write to a file or cloud service.

### Phase 2: Feature Implementation
- [ ] **Task 2.1: Implement Contact Management:**
    - [ ] Create API endpoints for inviting, viewing, and managing contacts (`/contacts`).
    - [ ] Implement logic for QR code/short code invitations.
- [ ] **Task 2.2: Implement Task Management:**
    - [ ] Build the API endpoints for CRUD operations on `Tasks` and `Subtasks` (`/tasks`, `/projects`).
    - [ ] Implement logic for different task categories (e.g., event, appointment, bill).
- [ ] **Task 2.3: Set up Local Docker Environment:**
    - [ ] Create a `docker-compose.yml` file.
    - [ ] Add services for the `api` and a `postgres` database.
    - [ ] Ensure the local environment can be started with `docker-compose up`.

### Phase 3: DevOps
- [ ] **Task 3.1: Configure CI/CD with GitHub Actions:**
    - [ ] Create a GitHub Actions workflow (`.github/workflows/ci.yml`).
    - [ ] The workflow should build the solution, run tests (once test projects are created), and build a Docker image for the API.