# ✅ Todo Web API

A Todo Web API built with **ASP.NET Core**, **Entity Framework Core**, and **PostgreSQL**.

The project follows a simple layered structure using Controllers, DTOs, a Service Layer, Dependency Injection, and EF Core migrations.

## 🚀 Tech Stack

- C#
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- Npgsql
- Swagger / OpenAPI
- Docker for PostgreSQL

## 📁 Project Structure

```text
Project/
├── Controllers/
│   └── TodoController.cs
│
├── Data/
│   └── AppDbContext.cs
│
├── DTO/
│   ├── CreateTodoDto.cs
│   └── TodoDto.cs
│
├── Models/
│   └── Todo.cs
│
├── Services/
│   ├── ITodoService.cs
│   └── TodoService.cs
│
├── Migrations/
├── Properties/
├── Program.cs
└── appsettings.json
```

### Controllers

Handles incoming HTTP requests and returns HTTP responses.

`TodoController` exposes the Todo API endpoints and delegates application logic to `ITodoService`.

### Services

Contains the application logic.

`ITodoService` defines the available Todo operations, while `TodoService` provides their implementation and communicates with the database through `AppDbContext`.

### Data

Contains the Entity Framework Core database context.

`AppDbContext` represents the connection between the application models and PostgreSQL.

### Models

Contains database entities.

The `Todo` entity is mapped by Entity Framework Core to the `Todos` table in PostgreSQL.

### DTO

Contains Data Transfer Objects used by the API.

- `CreateTodoDto` defines the data accepted when creating a Todo.
- `TodoDto` defines the data returned to the client.

DTOs keep the API contract separate from database entities.

## 🏗️ Architecture

The application follows this request flow:

```text
HTTP Request
     ↓
TodoController
     ↓
ITodoService
     ↓
TodoService
     ↓
AppDbContext
     ↓
Entity Framework Core
     ↓
Npgsql
     ↓
PostgreSQL
```

Dependencies are provided using the built-in ASP.NET Core Dependency Injection container.

## 📝 Todo Model

A Todo contains:

| Property | Type | Description |
|---|---|---|
| `Id` | `int` | Automatically generated identifier |
| `Title` | `string` | Todo title |
| `Description` | `string` | Todo description |
| `LikeCount` | `int` | Number of likes |
| `CreatedAt` | `DateTime` | Creation timestamp |
| `UpdatedAt` | `DateTime` | Last update timestamp |

When a Todo is created:

- `Id` is generated automatically.
- `LikeCount` starts at `0`.
- `CreatedAt` is set by the server.
- `UpdatedAt` is set by the server.

## 🔗 API Endpoints

### Create Todo

```http
POST /Todo
```

Request body:

```json
{
  "title": "Learn EF Core",
  "description": "Learn migrations"
}
```

Example response:

```json
{
  "id": 1,
  "title": "Learn EF Core",
  "description": "Learn migrations",
  "likeCount": 0,
  "createdAt": "2026-09-19T12:00:00Z",
  "updatedAt": "2026-09-19T12:00:00Z"
}
```

### Get All Todos

```http
GET /Todo
```

Returns all Todos stored in PostgreSQL.

### Get Todo by ID

```http
GET /Todo/{id}
```

Returns the requested Todo.

If the Todo does not exist:

```http
404 Not Found
```

Getting a Todo does **not** increase its like count.

### Like Todo

```http
POST /Todo/{id}/like
```

Increases the Todo's `LikeCount` by one and updates `UpdatedAt`.

If the Todo does not exist:

```http
404 Not Found
```

## 🗄️ Database

The project uses PostgreSQL with Entity Framework Core and the Npgsql provider.

Example connection string:

```text
Host=localhost;Port=5433;Database=TodoDb;Username=admin;Password=YOUR_PASSWORD
```

The database schema is managed through **EF Core migrations** rather than manually creating tables with SQL.

Create a migration:

```bash
dotnet ef migrations add InitialCreate
```

Apply migrations:

```bash
dotnet ef database update
```

EF Core creates and manages the `Todos` table based on the application model.

## ⚙️ Configuration

The database connection is configured in `appsettings.json` under:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5433;Database=TodoDb;Username=admin;Password=YOUR_PASSWORD"
  }
}
```

> ⚠️ Do not commit real production credentials or reusable passwords to a public repository. Use environment variables, User Secrets, or another secrets management solution for real applications.

## 🧪 Swagger

Swagger UI is enabled in the Development environment.

After starting the application, open:

```text
http://localhost:<port>/swagger
```

Swagger can be used to inspect and test all available endpoints directly from the browser.

## ▶️ Running the Project

Make sure PostgreSQL is running and the connection string contains the correct database credentials.

Restore dependencies:

```bash
dotnet restore
```

Apply database migrations:

```bash
dotnet ef database update
```

Run the API:

```bash
dotnet run
```

Then open Swagger using the URL shown in the application output.

## 🧠 Key Concepts Demonstrated

This project demonstrates:

- Controller-based ASP.NET Core Web APIs
- HTTP API endpoints
- Entity Framework Core
- PostgreSQL integration with Npgsql
- Entity-to-table mapping
- EF Core migrations
- DTOs
- Service Layer
- Interfaces
- Dependency Injection
- Constructor Injection
- HTTP status codes
- Swagger / OpenAPI
- Separation of HTTP, application, and data-access responsibilities

## 🔮 Possible Improvements

The project can be extended with:

- Async database operations
- Todo update and delete endpoints
- Input validation
- Todo completion status
- Filtering and sorting
- Pagination
- Global exception handling
- Authentication and authorization
- Automated tests