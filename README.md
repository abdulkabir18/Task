# 📝 Task Management API

A production-ready Task Management RESTful API built with C#, **.NET 8**, and **MySQL**, using **Hexagonal Architecture**.  
This API supports user registration, authentication, role-based authorization, task management, and real-time notifications.

---

## 🚀 Features

- ✅ User Registration & Login (JWT-based)
- ✅ Role-based Authorization (Admin, User)
- ✅ Task CRUD (Create, Read, Update, Delete)
- ✅ Task Assignment and Filtering
- ✅ Real-time Notifications (via SignalR)
- ✅ Global Error Handling Middleware
- ✅ FluentValidation for input validation
- ✅ Rate Limiting (Fixed Window)
- ✅ In-Memory Caching (`IMemoryCache`)
- ✅ Domain Events & Clean Architecture Principles
- ✅ Swagger/OpenAPI Documentation
- ✅ MySQL with EF Core
- ✅ Async, Scalable, and Secure Codebase

---

## 📦 Technologies Used

| Layer         | Tech Stack                              |
|---------------|------------------------------------------|
| Language      | C# (.NET 8)                              |
| Database      | MySQL                                    |
| ORM           | Entity Framework Core                    |
| Authentication| JWT, ASP.NET Identity, IPasswordHasher   |
| API           | MediatR, Controllers, Swagger UI         |
| Validation    | FluentValidation                         |
| Architecture  | Hexagonal / Clean Architecture           |
| Caching       | IMemoryCache                             |
| Rate Limiting | ASP.NET Rate Limiting Middleware         |
| Notifications | SignalR, In-App Notifications            |
| Utilities     | IHttpContextAccessor, Domain Events      |

---

## 📁 Project Structure (Hexagonal Style)

📦 1791Task
├── 📁 Application # Core business logic (use cases)
│ ├── 📁 Abstractions # Interfaces for Repositories, Services, UoW, etc.
│ ├── 📁 DTOs # Request/Response models
│ ├── 📁 Features # Use cases grouped by modules (e.g., Tasks, Auth)
│ ├── 📁 Extensions # Extension methods for the application layer
│ └── 📁 Validators # FluentValidation classes

├── 📁 Domain # Enterprise/business core (Entities, Enums, Events)
│ ├── 📁 Entities # Core entities (User, TaskItem, Notification)
│ ├── 📁 ValueObjects # E.g. Email, TaskStatus
│ ├── 📁 Events # Domain Events
│ └── 📁 Enums # Statuses, Roles, etc.

├── 📁 Infrastructures # EF Core, Auth, Caching, Repos (implements Application.Abstractions)
│ ├── 📁 DBaseContext # EF Core DbContext
│ ├── 📁 Repositories # TaskRepository, UserRepository, etc.
│ ├── 📁 Services # JwtService, CurrentUserService
│ ├── 📁 Caching # MemoryCacheService
│ ├── 📁 Configurations # EntityTypeConfigurations
│ └── 📁 Extensions # EF Core or DI extensions

├── 📁 Host (or API) # ASP.NET Core layer (entry point)
│ ├── 📁 Controllers # UserController, TaskController, etc.
│ ├── 📁 Hubs # SignalR Hubs
│ ├── 📁 Middlewares # Error handling, Logging, etc.
│ ├── 📁 Extensions # DI setup, Swagger, Rate Limiting
│ ├── 📁 Filters # Action filters, authorization policies
│ ├── appsettings.json # Base configuration
│ ├── Program.cs # App entry point
│ └── Host.csproj

## 📌 Note

This project follows a strict separation of concerns between layers:
- `Domain` is the center, containing pure business rules
- `Application` drives the use cases and orchestrates logic
- `Infrastructures` implements actual data access and services
- `Host` is the only layer that talks to the outside world (HTTP, SignalR)
