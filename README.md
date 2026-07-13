# TaskFlow – Personal Task Management System

TaskFlow è una piattaforma full-stack per la gestione personale di progetti e task.

Permette agli utenti di creare progetti, organizzare attività, monitorare lo stato dei task e gestire le proprie scadenze.

---

## Tech Stack

### Backend
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Authentication
- Swagger

### Frontend
- Angular
- TypeScript
- Tailwind CSS

---

## Architecture

Backend sviluppato con Layered Architecture:

Controller → Service → Repository → DbContext → Database

---

## Features

### Authentication
- Register / Login
- JWT Authentication

### Projects
- Creazione progetti
- Modifica ed eliminazione
- Visualizzazione progetti personali

### Tasks
- CRUD completo
- Stato task (Todo, InProgress, Done)
- Priorità
- Scadenze
- Ricerca e filtri

### Dashboard
- Statistiche task
- Monitoraggio attività

---

## Future Improvements

- Docker
- Pagination
- Sorting
- Deploy Cloud