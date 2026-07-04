# TaskFlow – Full Stack Task Management System

TaskFlow è una piattaforma full-stack per la gestione di progetti e task, sviluppata con ASP.NET Core e Angular.  
Il progetto simula un sistema reale di project management con autenticazione JWT, gestione progetti, task e membri.

---

## Tech Stack

### Backend (in corso)
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Authentication
- Swagger

### Frontend (dopo)
- Angular
- TypeScript
- Tailwind CSS

---

## Architettura

Backend basato su **Layered Architecture**:

Controllers → Services → Repositories → DbContext → Database

### Struttura progetto

- Controllers  
- Services  
- Interfaces  
- Repositories  
- DTOs  
- Models  
- Data  
- Authentication  
- Program.cs  

---

## Authentication

Sistema basato su **ASP.NET Core Identity + JWT**

### Features
- Register user
- Login user
- JWT token generation
- Password hashing
- Claims-based authentication

---

## Core Entities

### User
- IdentityUser extension
- FirstName, LastName
- Owned Projects
- Assigned Tasks

### Project
- Name, Description
- Owner
- Members (Many-to-Many)
- Tasks

### TaskItem
- Title, Description
- Status (Todo / InProgress / Done)
- Priority (Low / Medium / High)
- DueDate
- Assigned User

---

## Features

### Authentication
- Register / Login
- JWT authentication

### Projects
- Create project
- Update project
- Delete project
- View user projects

### Tasks
- CRUD operations
- Assign user
- Status & priority management
- Due date tracking

### Planned Features
- Filtering (status, priority, user, project)
- Sorting
- Pagination
- Dashboard stats

...
