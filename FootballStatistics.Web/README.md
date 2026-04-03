
⚽ FootballStatistics

## 📌 Overview

FootballStatistics is a web application built with ASP.NET Core MVC that allows users to explore football leagues, teams, players, stadiums, and matches.

The application provides role-based access:

- Guests can browse basic information  
- Registered users can view detailed data and standings  
- Administrators can manage all entities  

---

## 🚀 Technologies Used

- ASP.NET Core MVC (.NET 6+)  
- Entity Framework Core  
- SQL Server  
- ASP.NET Core Identity  
- Bootstrap  

---

## 🧱 Architecture

The project follows a layered architecture:

- Web – Controllers and Views (UI)  
- Services – Business logic  
- Data – DbContext, entities, migrations  
- ViewModels – Data transfer between layers  
- Common – Constants and shared logic  

---

## 📊 Features

### Public (Guest)

- View lists of:
  - Leagues
  - Teams
  - Players
  - Matches
  - Stadiums

### Registered Users

- View:
  - Details pages
  - League standings

### Administrator

- Full CRUD operations:
  - Create
  - Edit
  - Delete
- Access to Admin Area dashboard

---

## 🔐 Authentication & Authorization

- ASP.NET Core Identity is used  
- Roles:
  - Administrator
  - User  
- Access is restricted using `[Authorize]` and role-based policies  

---

## ⚙️ Business Logic

### Match Restrictions

- Matches can only be created between teams from the same league  

### Team Statistics

- Points and goals are calculated dynamically from match results  
- This ensures consistent and accurate standings  

---

## 🧪 Testing

Unit tests are implemented using:

- xUnit  
- EF Core InMemory Database  

Tested services:

- LeagueService  
- PlayerService  
- StadiumService  
- TeamService  
- MatchService  

---

## ⚠️ Error Handling

Custom error pages are implemented:

- 404 – Page Not Found  
- 500 – Server Error  

---

## 🛠️ Setup Instructions

1. Clone the repository  
2. Open the solution in Visual Studio  
3. Update the database:
`Update-Database`
4. Run the project  

---

## 🌐 Live Demo

👉 https://footballstatistics-lyuben-fahdgxehgczebybe.westeurope-01.azurewebsites.net/

---

## 🔐 Default Admin Account

Email: admin@footballstatistics.com  
Password: Admin123!  

---

## 🌐 Deployment

The application can be deployed to:

- Azure App Service  
- IIS  

---

## 📌 Notes

- The application is designed for educational purposes  
- Focus is on clean architecture, validation, and role-based access  
- The database is already deployed on Azure  
- No manual migration is required for testing the live demo  