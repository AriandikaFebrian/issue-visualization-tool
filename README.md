# Issue Visualization Tool

Project internal untuk visualisasi dan pelaporan tiket helpdesk berbasis OTRS. Terdiri dari frontend (React) dan backend (.NET Web API).

## 🧩 Teknologi yang Digunakan

- **Frontend**: React (Vite), Material UI, TanStack Query
- **Backend**: .NET 7 Web API, CQRS, Domain-Driven Design (DDD)
- **Database**: PostgreSQL / SQL Server
- **Visualisasi Data**: Power BI (opsional)
- **Tools**: Git, Azure DevOps

---

## 📦 Struktur Folder

- `frontend/` – UI interaktif untuk menampilkan data tiket
- `backend/` – REST API untuk ambil data dari OTRS
- `.gitignore` – Gabungan ignore frontend + backend
- `README.md` – Penjelasan proyek

---

## 🚀 Menjalankan Proyek

### 1. Frontend
```bash
cd frontend
npm install
npm run dev
