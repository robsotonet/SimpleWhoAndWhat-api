Great direction 🚀 — what you want is essentially a master prompt / work plan that an AI code-generation assistant (like Copilot, Cursor, Aider, or OpenAI’s own AI coding tools) can follow to scaffold, implement, and maintain the WhoAndWhat project according to the specs we’ve designed.
Here’s a structured work plan you can feed into your AI tool:

📌 AI Work Plan – WhoAndWhat App Development
1. Project Overview
Develop a task & contact management application called WhoAndWhat, consisting of:
        • Backend: ASP.NET Core Web API (.NET 8), PostgreSQL, Clean Architecture, JWT Auth, Serilog logging, Swagger/OpenAPI docs.
        • Web Client: React + TypeScript + TailwindCSS, API integration, PWA-ready.
        • Mobile Client: React Native (Expo), push notifications, shared logic with web when possible.
        • Deployment: Docker for local dev, GitHub Actions for CI/CD, production deploy to Azure or AWS.
Core Features:
        • User accounts (registration, login, subscription tier: free vs premium)
        • Contacts (invite via QR/code, association, blocking)
        • Tasks with categories (task, idea, event, appointment, medical, bill, project with subtasks, shopping, requests)
        • Calendar (personal + shared)
        • Messaging (internal + optional email CC, auto-expiration)
        • Notifications (reminders + push)

2. Backend (.NET 8 API with PostgreSQL)
        1. Setup & Architecture
                ○ Create solution WhoAndWhat.sln with projects:
                        § WhoAndWhat.Api (controllers, DI setup, Swagger)
                        § WhoAndWhat.Application (CQRS, business logic, DTOs, MediatR)
                        § WhoAndWhat.Domain (entities, enums, aggregates)
                        § WhoAndWhat.Infrastructure (EF Core, PostgreSQL, Serilog, email provider, Redis optional)
                ○ Add Swagger (Swashbuckle), Serilog, FluentValidation.
        2. Authentication
                ○ JWT (access token 1h, refresh token 30d)
                ○ Register, login, refresh, password reset endpoints
        3. Entities (EF Core + PostgreSQL)
                ○ Users, Contacts, Tasks, Subtasks, ShoppingLists, ShoppingItems, Messages, Notifications
                ○ Apply migrations with EF Core
        4. API Endpoints
                ○ Implement per API draft v0.1 (Auth, Users, Contacts, Tasks, Projects, Shopping, Requests, Calendar, Messaging, Notifications)
                ○ Enforce free vs premium tier limits via middleware
        5. Testing
                ○ Unit tests (xUnit + Moq)
                ○ Integration tests (WebApplicationFactory)
                ○ Swagger/Postman collections

3. Deployment & DevOps
        1. Local Dev
                ○ Docker Compose: api, postgres, pgadmin, redis (optional)
                ○ Run web + mobile clients separately
        2. CI/CD
                ○ GitHub Actions: build, run tests, build Docker images
                ○ Push to container registry (Azure ACR / AWS ECR)
        3. Production
                ○ Deploy API + Postgres in Azure (App Service + Azure Database for PostgreSQL) or AWS ECS/RDS
                ○ Configure monitoring (Application Insights or CloudWatch)
                ○ Use Key Vault/Secrets Manager for JWT keys + DB creds

4. Premium vs Free Enforcement
        • Free tier: max 20 contacts + 100 tasks
        • Premium: unlimited + projects with subtasks + advanced reminders + shared calendar + email CC in messages

5. AI Coding Instructions
When generating code, follow these rules:
        • Use Clean Architecture layering (API → Application → Domain → Infrastructure).
        • Use DTOs from the API draft for request/response contracts.
        • Write EF Core entities that map cleanly to PostgreSQL.
        • Always generate migrations when entities change.
        • Write unit tests for services, integration tests for endpoints.
        • Ensure Swagger/OpenAPI is kept updated.
        • Use dependency injection for all services.
        • Use async/await for all DB + API operations.
