# Smart Maintenance Management System (GMAO)

## Overview

Smart Maintenance Management System (GMAO) is a web application developed to manage industrial equipment maintenance operations efficiently.

The application allows users to:

* Manage equipment
* Track breakdowns and incidents
* Plan maintenance tasks
* Monitor interventions
* Manage users and roles
* Receive notifications
* Interact with an intelligent intent-based assistant

---

## Technologies

### Backend

* ASP.NET Core 8
* Entity Framework Core
* MySQL
* JWT Authentication
* Swagger / OpenAPI

### Architecture

* Repository Pattern
* Service Layer
* REST API
* Dependency Injection

---

## Main Features

* Authentication and authorization
* User management
* Equipment management
* Breakdown management
* Preventive maintenance
* Corrective maintenance
* Dashboard and statistics
* Notification system
* Intelligent intent assistant

---

## Project Structure

```text
Controllers/
DTOs/
Models/
Services/
Repositories/
Data/
Enums/
Options/
Migrations/
Program.cs
```

---

## Configuration

Before running the project, update `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=gmao_intent_db;user=YOUR_USER;password=YOUR_PASSWORD"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY"
  }
}
```

---

## Run Project

```bash
dotnet restore
dotnet run
```

---

## Author

Abdelkarim Lazar

GitHub:
https://github.com/abdelkarimlazar

LinkedIn:
https://www.linkedin.com/in/abdelkarim-lazar-8597173b5/
