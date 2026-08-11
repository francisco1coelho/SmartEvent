# SmartEvent API

SmartEvent is a REST API built with **.NET 10** for managing events, reservations, and users. The project follows Clean Architecture principles, ensuring a separation of concerns and a highly maintainable codebase.

> 📚 This is a personal learning project, built to practice and showcase enterprise-level .NET development practices (Clean Architecture, EF Core, JWT auth, CQRS, testing, CI/CD) as part of my portfolio.

## 🛠 Tech Stack
The following technologies and libraries are currently implemented and in use:
- **.NET 10** (ASP.NET Core Web API)
- **Entity Framework Core 10**
- **PostgreSQL** (via `Npgsql.EntityFrameworkCore.PostgreSQL`)
- **Swagger / OpenAPI** for API documentation 

## 📂 Architecture & Folder Structure
The project is strictly structured around the Clean Architecture model, divided into the following projects:

- **Domain** (`SmartEvent.Domain`)
  - `Entities`: Contains the core domain models (`User`, `Event`, `Category`, `Reservation`).
  - `Enums`: Includes enums like `EventState`, `ReservationState`, and `Role`.
- **Application** (`SmartEvent.Application`)
  - `DTOs`: Data Transfer Objects used to shape the data between the API and Application layers.
  - `Interfaces`: Contains Repository and Unit of Work abstractions (`IRepository`, `IUnitOfWork`, etc.).
  - `Services`: Business logic implementation for entities (`UserService`, `EventService`, etc.).
- **Infrastructure** (`SmartEvent.Infrastructure`)
  - `Persistence`: EF Core DbContext (`SmartEventDbContext`) and Fluent API Configurations.
  - `Repositories`: Concrete implementations of data access abstractions.
  - `Migrations`: EF Core database migration files.
- **API** (`SmartEvent.API`)
  - `Controllers`: Exposes the HTTP endpoints (`UsersController`, `EventsController`, `CategoriesController`, `ReservationsController`).
  - Dependency Injection setup and ASP.NET Core middleware pipeline (`Program.cs`).

## 🚀 Setup & How to Run Locally

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker & Docker Compose](https://www.docker.com/)

### Instructions

1. **Spin up the database using Docker Compose**
   Assuming you have a standard setup (or using the provided `docker-compose.yml`), start the PostgreSQL container:
   ```bash
   docker-compose up -d
   ```

2. **Apply Database Migrations**
   Navigate to the API folder and run EF Core migrations to ensure your Postgres schema is updated:
   ```bash
   cd API
   dotnet ef database update --project ../Infrastructure/SmartEvent.Infrastructure.csproj
   ```

3. **Run the API**
   Start the application:
   ```bash
   dotnet run --launch-profile "https"
   ```
   *The Swagger interface will be available at `https://localhost:<port>/swagger`.*

## 🗺 Roadmap & Progress

- [x] **Foundations** — Clean Architecture structure, Domain entities, EF Core setup, Repository + Unit of Work pattern, Docker Compose, initial migrations, basic CRUD endpoints for Users, Events, Categories
- [ ] **Users** — CRUD endpoints, profile management
- [ ] **Reservations** — Booking logic (capacity control, duplicate prevention, cancellation rules), endpoints
- [ ] **Authentication** — JWT + refresh tokens, role-based authorization
- [ ] **Background Jobs** — Scheduled emails (confirmation, reminders) via Hangfire
- [ ] **Testing & CI** — Unit/integration tests, GitHub Actions pipeline
- [ ] **Analytics** — Occupancy rates, booking trends, admin dashboard endpoints
