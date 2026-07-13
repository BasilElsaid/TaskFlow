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

## Docker

L'applicazione può essere avviata con Docker Compose, includendo frontend Angular, API .NET e SQL Server.

```bash
cp .env.example .env
docker compose up --build
```

Il comando `cp .env.example .env` crea il file `.env` locale con le variabili necessarie a Docker.  
Prima dell’avvio, sostituisci i valori di esempio con una password SQL Server e una chiave JWT sicure.

Una volta avviata:

- Frontend: `http://localhost:4200`
- API / Swagger: `http://localhost:5271/swagger`

---

## Future Improvements

- Pagination
- Sorting
- Deploy Cloud
