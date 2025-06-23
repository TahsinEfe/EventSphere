<h1 align="center">🎉 EventSphere – Comprehensive Event Management Platform</h1>
<p align="center">
  A full-stack web application for seamless event planning and participation, built with ASP.NET Core &amp; React.
</p>

---

## 📑 Table of Contents
1. [Features](#-features)
2. [Database Schema](#-database-schema)
3. [API Endpoints](#-api-endpoints)
4. [UI Components](#-ui-components)
5. [Architecture & Tech Stack](#-architecture--tech-stack)
6. [Getting Started](#-getting-started)
7. [Deployment](#-deployment)
8. [Security](#-security)
9. [Performance Optimizations](#-performance-optimizations)
10. [Testing](#-testing)
11. [Contributing](#-contributing)
12. [License](#-license)
13. [Author](#-author)
14. [Acknowledgments](#-acknowledgments)
15. [Support](#-support)

---

## 🌟 Features

### 👥 User Management
- **Registration & Authentication** (JWT)
- **Role-Based Access Control** – Admin • Organization Member • User
- **Profile Management** & personalized dashboard

### 🎫 Event Management
- Rich **event creation** (images, types, statuses)
- Advanced **event discovery** & filtering
- **Seat reservation** with dynamic layouts
- **Feedback & ratings** after events

### 🏢 Organization Tools
- Organization CRUD & member management
- Task assignment & progress tracking
- Event analytics dashboards

### 🔔 Notification System
- Real-time platform notifications
- Contact-form messages delivered as notifications

---

## 🗄️ Database Schema

### Core Entities
| Table            | Description                          |
|------------------|--------------------------------------|
| **Users**        | User accounts & authentication       |
| **Roles**        | Permission levels                    |
| **Organizations**| Event-organizing entities            |
| **Events**       | Main event records                   |
| **EventTypes**   | Event categorization                 |
| **EventStatuses**| Event lifecycle states               |
| **Addresses**    | Event locations                      |
| **Seats**        | Seating arrangements                 |
| **Feedbacks**    | User reviews & ratings               |
| **Tasks**        | Event-related task management        |
| **Notifications**| Platform notifications               |

### Key Relationships
- `Users` ↔ `Organizations` – **M:N** via **OrganizationMembers**
- `Events` ↔ `Organizations` – **N:1**
- `Events` ↔ `Addresses` – **N:1**
- `Events` ↔ `Seats` – **1:N**
- `Users` ↔ `Events` – **M:N** via **Feedbacks**

---

## 🔧 API Endpoints

> Full, interactive docs available at `/swagger`

| Resource | Method | Endpoint | Description |
|----------|--------|----------|-------------|
| **Auth** | `POST` | `/api/auth/login` | User login |
|          | `POST` | `/api/auth/register` | User registration |
| **Events** | `GET` | `/api/events` | List events |
|           | `POST`| `/api/events` | Create event |
|           | `GET` | `/api/events/{id}` | Event details |
|           | `PUT` | `/api/events/{id}` | Update event |
|           | `DELETE` | `/api/events/{id}` | Delete event |
| **Users** | `GET` | `/api/users` | List users (admin) |
|           | `GET` | `/api/users/{id}` | User details |
|           | `PUT` | `/api/users/{id}` | Update profile |
| **Organizations** | `GET` | `/api/organizations` | List organizations |
|                 | `POST`| `/api/organizations` | Create organization |
|                 | `PUT` | `/api/organizations/{id}` | Update organization |

---

## 🎨 UI Components

| Area            | Highlights |
|-----------------|------------|
| **Design System** | Dark & light themes, Tailwind utility spacing, accessible components |
| **Home**        | Landing page with featured events |
| **Events**      | Discovery & filter panel |
| **Event Detail**| Gallery, description, seat map, feedback |
| **Dashboard**   | Role-specific management views |
| **Profile**     | User information & preferences |

---

## 🏗️ Architecture & Tech Stack

### Backend (.NET Core)
- **ASP.NET Core 9** • **Entity Framework Core** • **SQL Server**
- **Swagger/OpenAPI** docs • **JWT** auth • **Docker-ready**

### Frontend (React)
- **React 18 + TypeScript + Vite**
- **TailwindCSS** + **shadcn/ui** components
- **React Router DOM** • **Axios** • **Zod** validation
- **Lucide React** icon set

### Dev & Ops
- **Visual Studio 2022 / VS Code**
- **Docker** containers • IIS or Nginx reverse proxy
- Cloud-ready for **Azure App Service**, **Vercel/Netlify** etc.

---

## 🚀 Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- SQL Server or SQL Server Express
- Git

### Backend Setup
```bash
git clone https://github.com/<your-user>/eventsphere.git
cd eventsphere/EventSphere
# Update connection string in appsettings.json
dotnet ef database update      # run migrations
dotnet run                     # launches on https://localhost:5172
```

###Frontend Setup
```bash
cd eventsphere/frontend
npm install
echo "VITE_API_URL=https://localhost:5172" > .env
npm run dev                     # http://localhost:8080
```

## ☁️ Deployment

| **Layer**   | **Options (Examples)**                               |
|-------------|------------------------------------------------------|
| **Backend** | Azure App Service • AWS EC2 • Heroku                 |
| **Frontend**| Vercel • Netlify • Azure Static Web Apps             |
| **Database**| Azure SQL Database • AWS RDS                         |
| **Containers** | Docker Compose • Azure Container Apps           |

---

## 🔒 Security

- ✅ Robust **input validation** and **DTO mapping**
- ✅ **Parameterized queries** to prevent SQL injection
- ✅ Strict **CORS** configuration
- ✅ **Role-based authorization** filters
- ✅ Secure **file uploads** for event-related media

---

## 📈 Performance Optimizations

- 🚀 Strategic **database indexing** for faster queries  
- 🚀 **Lazy loading** & **code splitting** in React frontend  
- 🚀 **Image compression** on upload  
- 🚀 **Caching** (both server-side & client-side)  
- 🚀 **Minified JS/CSS** bundles for faster loading

---

## 🧪 Testing

### 🧪 Backend
- **xUnit** / **MSTest** for unit & integration tests
- **Mock repositories** for isolated service testing

### 🧪 Frontend
- **Vitest** for unit tests
- **React Testing Library** for component testing
- **Cypress** for end-to-end (E2E) testing



