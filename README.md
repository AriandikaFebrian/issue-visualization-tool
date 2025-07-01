🐛 BugNest - Issue Visualization Tool



BugNest is an internal platform for tracking and visualizing software issues and bugs across teams and projects. It provides a modern UI and robust backend API to simplify collaboration, improve transparency, and streamline issue management. Built to empower development teams with clear visibility into their work through structured project spaces, smart tagging, real-time activity logs, and insightful summaries.

📦 Tech Stack Overview

🖼 Frontend

Framework: React with Vite — fast, modern build tool

Language: TypeScript

UI Kit: Material UI 5

State/Async Handling: TanStack Query for fetching/caching

Routing: React Router DOM

Authentication: JWT Token stored in HTTP Headers

Utilities: Axios, Context API, Zustand (if needed)

🧠 Backend

Framework: ASP.NET Core 7 Web API

Architecture: Clean Architecture + CQRS (Command Query Responsibility Segregation)

Mediator Pattern: MediatR

ORM: Entity Framework Core

Auth: JWT Bearer Authentication

Database: PostgreSQL / SQL Server

Validation: FluentValidation

Logging: Serilog (recommended)

Documentation: Swagger (Swashbuckle)

🚀 Getting Started

🖥 Frontend Setup (React Vite)

cd frontend
npm install
npm run dev

Visit: http://localhost:5173

Key Features:

Auto-login with JWT

Sidebar + top navbar layout

Activity Feed from backend

Public and private projects

Tag management modal (connected to backend)

⚙️ Configure Axios with the correct base URL for your backend (e.g., http://localhost:5001).

🧠 Backend Setup (.NET Web API)

cd backend
# Using .NET CLI
dotnet build

# Apply migrations
dotnet ef database update

# Run
dotnet run

API available at: https://localhost:5001/api

Environment Settings:

Ensure valid appsettings.Development.json

JWT secret and connection strings must be configured properly

🔐 Authentication

JWT-based auth system with secure claims

Login returns access_token

Claims:

nameid: UserId

nrp: Employee NRP

All authorized requests must include:

Authorization: Bearer <your-jwt-token>

📌 API Modules Summary

🔑 Auth

POST /api/auth/register

POST /api/auth/login

GET /api/auth/me

PUT /api/auth/me

POST /api/auth/upload — Upload profile picture

📁 Project Management

POST /api/project — Create a new project

GET /api/project/mine — Get all projects owned by current user

GET /api/project/{projectCode} — Project detail + save to recent

GET /api/project/{projectCode}/summary

GET /api/project/{projectCode}/members — List all members in a project

POST /api/project/{projectCode}/members — Add member (only by project owner)

GET /api/project/summaries — Get all project summaries

GET /api/project/recent — Recently opened projects

GET /api/project/projects/public-feed — Publicly visible projects

GET /api/project/{projectCode}/details — Summary for project dashboard

🧪 Tags

POST /api/tag — Create a tag

Automatically injects createdByNRP from token claim

🐛 Issues

POST /api/issue — Create new issue

GET /api/issue/by-code/{projectCode} — Get issues by project

PUT /api/issue/{issueCode}/assign-users — Assign NRP(s) to issue

PATCH /api/issue/{issueCode}/status — Change status of issue

GET /api/issue/{issueCode}/history — List history of an issue

GET /api/issue/recent — Get most recently created issues

GET /api/issue/assigned — Get issues assigned to current user

💬 Comments

GET /api/comments/issue/{issueCode} — Get all comments under an issue

POST /api/comments — Add comment to issue (requires NRP in token)

🔔 Notifications

GET /api/notification — All notifications for logged-in user

GET /api/notification/unread — Only unread notifications

PATCH /api/notification/{id}/read — Mark one as read

PATCH /api/notification/read-all — Mark all as read

📜 Activity Logs

GET /api/activities — Paginated activity list

GET /api/activities/{id} — Detailed view per activity

📊 Power BI Compatibility (Planned)

Expose raw or pre-aggregated endpoints for visualizing trends:

Issue volume

Tag usage

Notification count

Project creation over time

📄 License

This repository is licensed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International (CC BY-NC-ND 4.0) license.

✅ Attribution required

🚫 No commercial use

🚫 No modifications or derivative works



👤 Author

Ariandika FebrianCreator & maintainer of BugNest — crafted for internal IT teams and developers aiming for streamlined issue visibility.

Want to contribute? Fork it, credit it, and use it internally — no problem.

📬 Contact

GitHub: @AriandikaFebrian

Email: available on request via GitHub profile

