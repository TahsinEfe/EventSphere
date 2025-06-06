# 🎉 EventSphere – Event Management Web Application

**EventSphere** is a full-stack event management web application that enables organizations and users to create, manage, discover, and participate in various types of events. It provides an intuitive interface, robust backend APIs, and flexible admin functionalities for streamlined event workflows.

---

## ✅ Features

### 👥 User Features
- Browse upcoming events with date, location, and type.
- Register for events directly through the web interface.
- Submit feedback and ratings after attending events.
- View personal registration history and upcoming events.

### 🏢 Organization Features
- Create, edit, and delete events with images, descriptions, and types.
- Assign locations and categorize events.
- Manage organization members and define admin roles.

### 🛠️ Admin Features
- View and manage all events and organizations on the platform.
- Access a full notification system (mark as read / delete).
- View and manage user feedback and system activity.

---

## 🧱 Technologies Used

### Backend
- **ASP.NET Core 7** – RESTful API development
- **Entity Framework Core** – ORM for database operations
- **SQL Server / SQLite** – Flexible database backend
- **Swagger** – Interactive API documentation
- **DTOs & ViewModels** – Clean architecture and data separation

### Frontend
- **React + Vite** – Fast and modern JavaScript framework
- **TypeScript** – Type-safe development
- **TailwindCSS** – Utility-first CSS for UI styling
- **ShadCN UI** – Reusable and accessible components
- **Axios** – API request handling

---

## 🖥️ How It Works

1. **Organizations** create and publish events from their dashboard.
2. **Users** browse and register for events, and give feedback after attendance.
3. **Admins** monitor platform-wide activity, events, and feedback through a centralized panel.
4. All communication is handled via a secure and scalable REST API.

---

## 🚀 How to Run the Application

### Backend Setup

```bash
cd EventSphere
dotnet restore
dotnet build
dotnet run
```

---

## 🎨 Frontend Setup

Make sure you have Node.js (version 16 or above) and npm installed.

```bash
cd frontend
npm install
npm run dev
```

- The frontend will run on: http://localhost:5173
- API requests are configured to target the backend at http://localhost:5172

---

## 📦 Optional: Production Build

To create a production-ready version of the frontend:

```bash
npm run build
```

This will generate a /dist folder that can be deployed to Vercel, Netlify, or any static hosting service.

---

## 📌 Deployment

This project is designed to be deployed as two separate services:

### Backend:
  - Render
  - Azure App Service
  - Docker container

### Frontend:
  - Vercel
  - Netlify
  - GitHub Pages (after npm run build)

---

## 🧑‍💻 Developer Info:

Created by: Tahsin Efe Yılmaz

- 📍 Turkey
- 🔗 GitHub: @TahsinEfe
