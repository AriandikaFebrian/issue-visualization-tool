[![License: CC BY-NC-ND 4.0](https://img.shields.io/badge/License-CC%20BY--NC--ND%204.0-lightgrey.svg)](https://creativecommons.org/licenses/by-nc-nd/4.0/)

# 🐛 BugNest — Issue Visualization Tool

**BugNest** is an internal platform designed to track and visualize software issues and bugs across teams and projects. It aims to improve collaboration, transparency, and efficiency in issue management for development teams.

---

## 🚀 Key Features

- 📌 **Project Dashboard** (public & private)
- 🧑‍💻 **Auto-login** with JWT
- 🧠 **Smart Tagging**
- 🔄 **Real-time Activity Feed**
- 🧪 **Issue Management** with user assignment & status tracking
- 💬 **Comment System**
- 🔔 **Notification Panel**
- 📊 **Power BI-compatible endpoints** *(planned)*

---

## 📦 Tech Stack

### 🖥 Frontend (React + Vite)
- Framework: `React + Vite`
- Language: `TypeScript`
- UI: `Material UI v5`
- State Management: `TanStack Query`, `Context API`, `Zustand (optional)`
- Routing: `React Router DOM`
- HTTP: `Axios` (JWT via headers)

### 🧠 Backend (.NET 7 Web API)
- Framework: `ASP.NET Core 7`
- Architecture: `Clean Architecture + CQRS`
- Patterns: `MediatR`, `FluentValidation`
- ORM: `Entity Framework Core`
- Database: `PostgreSQL` / `SQL Server`
- Authentication: `JWT Bearer`
- Documentation: `Swagger (Swashbuckle)`
- Logging: `Serilog`

---

## 🧑‍💻 Getting Started

### 🔧 Frontend Setup

```bash
cd frontend
npm install
npm run dev
```

📍 Visit: [http://localhost:5173](http://localhost:5173)

> Make sure to configure Axios base URL to your backend (e.g., `http://localhost:5001`)

### 🔧 Backend Setup

```bash
cd backend
dotnet build
dotnet ef database update
dotnet run
```

📍 API available at: [https://localhost:5001/api](https://localhost:5001/api)

> Ensure your `appsettings.Development.json` contains:
> - Valid JWT secret
> - Correct database connection string

---

## 🔐 Authentication (JWT)

After login, an `access_token` is returned. Include it in all authenticated requests:

```http
Authorization: Bearer <your-jwt-token>
```

### 🎫 JWT Claims

| Claim    | Description    |
|----------|----------------|
| `nameid` | User ID        |
| `nrp`    | Employee NRP   |

---

## 📌 API Endpoints Overview

### 🔑 Auth
- `POST /api/auth/register` — Register
- `POST /api/auth/login` — Login
- `GET /api/auth/me` — Get current user profile
- `PUT /api/auth/me` — Update profile
- `POST /api/auth/upload` — Upload profile picture

### 📁 Project Management
- `POST /api/project` — Create project
- `GET /api/project/mine` — Get user-owned projects
- `GET /api/project/{code}` — Get project details
- `GET /api/project/{code}/summary` — Project summary
- `GET /api/project/summaries` — All project summaries
- `GET /api/project/projects/public-feed` — Public projects
- `POST /api/project/{code}/members` — Add member
- `GET /api/project/{code}/members` — List project members

### 🧪 Tags
- `POST /api/tag` — Create tag (auto-injects `createdByNRP` from JWT)

### 🐛 Issues
- `POST /api/issue` — Create issue
- `GET /api/issue/by-code/{projectCode}` — Get project issues
- `PUT /api/issue/{code}/assign-users` — Assign users
- `PATCH /api/issue/{code}/status` — Change issue status
- `GET /api/issue/{code}/history` — Issue history
- `GET /api/issue/assigned` — Assigned to current user
- `GET /api/issue/recent` — Recent issues

### 💬 Comments
- `GET /api/comments/issue/{code}` — Get comments for an issue
- `POST /api/comments` — Add comment (requires NRP in token)

### 🔔 Notifications
- `GET /api/notification` — All notifications
- `GET /api/notification/unread` — Unread only
- `PATCH /api/notification/{id}/read` — Mark as read
- `PATCH /api/notification/read-all` — Mark all as read

### 📜 Activity Logs
- `GET /api/activities` — Paginated list
- `GET /api/activities/{id}` — Activity detail

### 📊 Power BI Integration *(Planned)*
- Expose data endpoints: issue trends, tag usage, notification count, project creation over time

---

## 📄 License

This software is licensed under the **Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License**.

[![License: CC BY-NC-ND 4.0](https://img.shields.io/badge/License-CC%20BY--NC--ND%204.0-lightgrey.svg)](https://creativecommons.org/licenses/by-nc-nd/4.0/)

> By using this software, you agree to the following terms:
> - Attribution — You must give appropriate credit to the original author: **Ariandika Febrian**
> - NonCommercial — You may not use the material for commercial purposes.
> - NoDerivatives — You may not distribute modified material.


This project is licensed under the **Creative Commons BY-NC-ND 4.0** license.

- ✅ Attribution required
- ❌ Non-commercial use only
- ❌ No derivative works allowed

---

## 👤 Author

**Ariandika Febrian**  
📌 Creator & Maintainer of BugNest  
🌟 Built for internal IT teams & developers who need clean issue visibility

📬 Contact:
- GitHub: [@AriandikaFebrian](https://github.com/AriandikaFebrian)
- Email: available via GitHub profile