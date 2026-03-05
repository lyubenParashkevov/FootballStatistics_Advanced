# FootballStatistics


FootballStatistics is an ASP.NET Core MVC web application for managing football leagues, teams, and matches.  
The project demonstrates the fundamentals of ASP.NET Core, Entity Framework Core, MVC architecture, Dependency Injection, and data validation.

---

## 📌 Project Overview

The application allows registered users to:

- Create and manage football leagues
- Create and manage teams within leagues
- Create and manage matches between teams
- View detailed statistics for leagues, teams, and matches

The project follows a layered architecture structure (Core, Infrastructure, Web) and applies SOLID principles and clean code practices.

---

## 🛠 Technologies Used

- ASP.NET Core (.NET 8)
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Razor Views
- Bootstrap 5
- Dependency Injection
- Asynchronous Programming (async/await)

---

## 🏗 Architecture

The project is structured in logical layers:

- **Core**
  - Service interfaces (Contracts)
  - Service implementations
  - Validation constants

- **Infrastructure**
  - Entity models
  - DbContext
  - Entity configurations (Fluent API)
  - Migrations

- **Web**
  - Controllers
  - ViewModels
  - Razor Views
  - Identity area

This structure ensures separation of concerns and maintainability.
