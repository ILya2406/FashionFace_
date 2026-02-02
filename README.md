# FashionFace Platform

AI-powered platform for fashion model portfolio management and AI-generated photoshoots.

## 📋 Table of Contents
- [Architecture Overview](#architecture-overview)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Development](#development)
- [API Documentation](#api-documentation)
- [Database Schema](#database-schema)
- [Known Issues](#known-issues)

---

## 🏗 Architecture Overview

FashionFace follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────────────────────────────────────────┐
│                    Presentation                      │
│  ┌────────────────┐  ┌─────────────────────────┐   │
│  │  Web API       │  │  SignalR Hubs           │   │
│  │  (REST)        │  │  (Real-time)            │   │
│  └────────────────┘  └─────────────────────────┘   │
└───────────────┬─────────────────┬───────────────────┘
                │                 │
┌───────────────▼─────────────────▼───────────────────┐
│               Application Layer                      │
│  ┌──────────┐  ┌──────────┐  ┌─────────────────┐  │
│  │Controllers│  │  Facades │  │  Background    │  │
│  │ (Routes) │  │(Use Cases│  │  Workers       │  │
│  └──────────┘  └──────────┘  └─────────────────┘  │
└───────────────┬──────────────────┬──────────────────┘
                │                  │
┌───────────────▼──────────────────▼──────────────────┐
│                 Domain Layer                         │
│  ┌──────────┐  ┌──────────┐  ┌─────────────────┐  │
│  │  Models  │  │ Services │  │  Interfaces     │  │
│  │(Entities)│  │(Business)│  │  (Contracts)    │  │
│  └──────────┘  └──────────┘  └─────────────────┘  │
└───────────────┬──────────────────┬──────────────────┘
                │                  │
┌───────────────▼──────────────────▼──────────────────┐
│              Infrastructure Layer                    │
│  ┌──────────┐  ┌──────────┐  ┌─────────────────┐  │
│  │PostgreSQL│  │ RabbitMQ │  │  External APIs  │  │
│  │(EF Core) │  │(MassTransit) │ (ImageKit, AI)│  │
│  └──────────┘  └──────────┘  └─────────────────┘  │
└──────────────────────────────────────────────────────┘
```

### Key Architectural Patterns

1. **Clean Architecture**
   - Domain-centric design
   - Dependency inversion (dependencies point inward)
   - Technology-agnostic core

2. **CQRS (Command Query Responsibility Segregation)**
   - Separate read/write repositories
   - Optimized queries for different use cases

3. **Event-Driven Architecture**
   - RabbitMQ for async messaging
   - Outbox pattern for reliable event publishing
   - Background workers for long-running tasks

4. **Repository Pattern**
   - Generic repositories for data access
   - Separation of concerns between data and business logic

5. **Facade Pattern**
   - Simplified interface to complex subsystems
   - Encapsulates business logic

---

## 🛠 Technology Stack

### Backend (.NET 10)
- **Framework**: ASP.NET Core 10.0
- **Language**: C# 13
- **ORM**: Entity Framework Core 10
- **API**: REST + SignalR (WebSockets)
- **Authentication**: JWT Bearer tokens
- **Messaging**: RabbitMQ + MassTransit
- **Cache**: Redis Stack
- **Database**: PostgreSQL 16

### Frontend (React + TypeScript)
- **Framework**: React 18 + Vite
- **Language**: TypeScript 5
- **UI**: Tailwind CSS
- **3D Rendering**: Three.js (for pose visualization)
- **State**: React Hooks
- **HTTP**: Axios
- **Real-time**: SignalR Client

### Infrastructure
- **Containerization**: Docker + Docker Compose
- **Reverse Proxy**: nginx
- **File Storage**: ImageKit CDN
- **Message Queue**: RabbitMQ (with Management UI)
- **Caching**: Redis Stack

---

## 📁 Project Structure

### Backend Structure

```
FashionFace_restored/
├── FashionFace.Common.*              # Shared utilities
│   ├── Constants/                    # Application constants
│   ├── Exceptions/                   # Custom exceptions
│   ├── Extensions/                   # Extension methods
│   └── Models/                       # Shared DTOs
│
├── FashionFace.Controllers.*         # API Layer
│   ├── Anonymous/                    # Public endpoints
│   ├── Authorized/                   # Authenticated endpoints
│   ├── Users/                        # User-specific endpoints
│   └── Admins/                       # Admin endpoints
│
├── FashionFace.Facades.*             # Application Logic Layer
│   ├── Anonymous/                    # Public use cases
│   ├── Authorized/                   # Auth use cases
│   ├── Users/                        # User use cases
│   │   ├── DossierEntities/         # Dossier management
│   │   ├── RenderPipelines/         # AI generation
│   │   └── Portfolios/              # Portfolio management
│   └── Admins/                       # Admin use cases
│
├── FashionFace.Repositories.*        # Data Access Layer
│   ├── Context/                      # EF Core DbContext
│   │   ├── Models/                  # Domain entities
│   │   │   ├── DossierEntities/    # Dossier models
│   │   │   ├── RenderPipelines/    # AI pipeline models
│   │   │   ├── PoseReferences/     # Pose reference models
│   │   │   └── MediaEntities/      # Media models
│   │   ├── Configurations/          # EF configurations
│   │   └── Migrations/              # Database migrations
│   ├── Implementations/             # Repository implementations
│   └── Read/                        # Read-only repositories
│
├── FashionFace.Dependencies.*        # External Dependencies
│   ├── Identity/                    # JWT authentication
│   ├── MassTransit/                 # RabbitMQ integration
│   ├── Redis/                       # Caching
│   ├── SkiaSharp/                   # Image processing
│   └── HttpClient/                  # HTTP utilities
│
├── FashionFace.Services.*            # Domain Services
│   ├── ConfigurationSettings/       # App settings
│   └── Singleton/                   # Singleton services
│
├── FashionFace.Executable.*          # Runnable Applications
│   ├── WebApi/                      # REST API server
│   │   ├── Program.cs              # Entry point
│   │   ├── appsettings.json        # Configuration
│   │   └── Dockerfile              # Docker image
│   ├── Worker.UserEvents/           # User event processor
│   │   ├── Handlers/               # Event consumers
│   │   ├── Program.cs
│   │   └── Dockerfile
│   └── Worker.Integration.AI/       # AI integration worker (🚧 WIP)
│       └── Handlers/
│           └── HandleRenderPipelineAttemptCreateRequestTaskConsumer.cs
│
├── docker-compose.yml                # Docker orchestration
├── CHANGELOG.md                      # Detailed change log
└── README.md                         # This file
```

### Frontend Structure

```
glamhub-profile-webapp/
├── frontend/
│   ├── src/
│   │   ├── api/
│   │   │   └── fashionface-client.ts    # API client
│   │   ├── components/
│   │   │   ├── PoseAngleViewer.tsx     # 3D pose viewer
│   │   │   └── ...
│   │   ├── pages/
│   │   │   ├── Profile.tsx             # User profile page
│   │   │   ├── AIGenerator.tsx         # AI generation UI
│   │   │   └── ...
│   │   ├── services/
│   │   │   └── signalr-service.ts      # Real-time messaging
│   │   └── main.tsx                     # Entry point
│   ├── public/
│   │   └── models/                      # 3D pose models (.obj)
│   └── package.json
└── docker-compose.yml
```

---

## 🚀 Getting Started

### Prerequisites
- Docker Desktop 4.x+
- .NET 10 SDK (for local development)
- Node.js 20+ (for frontend development)

### Quick Start (Docker)

1. **Clone repositories**
   ```bash
   git clone https://github.com/ILya2406/FashionFace_.git
   git clone https://github.com/ILya2406/glamhub-prfile_actual.git
   ```

2. **Start backend services**
   ```bash
   cd FashionFace_restored
   docker compose up -d
   ```

   Services will be available at:
   - REST API: http://localhost:5000
   - SignalR Hubs: http://localhost:8090
   - RabbitMQ Management: http://localhost:15672 (guest/guest)
   - Redis Insight: http://localhost:8001

3. **Start frontend**
   ```bash
   cd glamhub-profile-webapp/frontend
   npm install
   npm run dev
   ```

   Frontend: http://localhost:5173

### Database Setup

Migrations are applied automatically on startup. To apply manually:

```bash
cd FashionFace.Repositories.Context
dotnet ef database update --startup-project ../FashionFace.Executable.WebApi
```

---

## 💻 Development

### Backend Development

1. **Build**
   ```bash
   dotnet build FashionFace.Executable.WebApi/FashionFace.Executable.WebApi.csproj
   ```

2. **Run locally**
   ```bash
   cd FashionFace.Executable.WebApi
   dotnet run
   ```

3. **Create migration**
   ```bash
   cd FashionFace.Repositories.Context
   dotnet ef migrations add MigrationName --startup-project ../FashionFace.Executable.WebApi
   ```

### Frontend Development

1. **Install dependencies**
   ```bash
   cd glamhub-profile-webapp/frontend
   npm install
   ```

2. **Development server**
   ```bash
   npm run dev
   ```

3. **Build for production**
   ```bash
   npm run build
   ```

---

## 📚 API Documentation

### Authentication

All authenticated endpoints require JWT Bearer token:
```
Authorization: Bearer <token>
```

### Key Endpoints

#### Public Endpoints
```
POST /api/v1/anonymous/login           - User login
GET  /api/v1/anonymous/profile/{id}    - Public profile
GET  /api/v1/anonymous/pose-reference/list - Available poses
```

#### User Endpoints (Authenticated)
```
# Profile Management
GET    /api/v1/user/profile              - Get own profile
PUT    /api/v1/user/profile              - Update profile
POST   /api/v1/user/profile/media        - Upload media

# Dossier Management
GET    /api/v1/user/dossier              - Get dossier
POST   /api/v1/user/dossier              - Create dossier
GET    /api/v1/user/dossier/media/list   - List dossier media
POST   /api/v1/user/dossier/media        - Add media to dossier
DELETE /api/v1/user/dossier/media        - Remove media from dossier

# AI Render Pipeline
POST   /api/v1/user/render-pipeline              - Create pipeline
POST   /api/v1/user/render-pipeline/attempt      - Start generation
GET    /api/v1/user/render-pipeline/attempt      - Get status
```

#### Admin Endpoints
```
GET  /api/v1/admin/users               - List all users
POST /api/v1/admin/users/{id}/ban      - Ban user
```

### Response Format

**Success Response:**
```json
{
  "data": { ... },
  "totalCount": 10
}
```

**Error Response:**
```json
{
  "message": "Error description",
  "code": "ERROR_CODE"
}
```

---

## 🗄 Database Schema

### Core Tables

#### Users & Authentication
- `ApplicationUser` - user accounts
- `ApplicationRole` - roles (User, Admin)

#### Profiles
- `Profile` - user profiles (1:1 with ApplicationUser)
- `Dossier` - model portfolios (1:1 with Profile)
- `DossierMediaAggregate` - media in dossier
- `AppearanceTraits` - physical characteristics

#### Media Management
- `MediaAggregate` - media metadata
- `MediaFile` - original files
- `OptimizedFile` - optimized versions
- `FileResource` - file storage info

#### AI Rendering (🚧 In Progress)
- `RenderPipeline` - generation pipeline
- `RenderPipelineAttempt` - generation attempt
- `RenderSucceededResult` / `RenderFailedResult` - results
- `PoseReference` - available poses

#### System
- `OutboxTask` - event publishing queue
- `NotificationOutbox` - notification queue

### Key Relationships

```
ApplicationUser 1─1 Profile 1─1 Dossier
                     │            │
                     │            └─ 1:N DossierMediaAggregate
                     │                         │
                     └─ 1:N ProfileMediaAggregate
                                  │
                                  └─ N:1 MediaAggregate
```

---

## ⚠️ Known Issues

### 🚧 AI Worker Not Deployed

**Status**: Partial implementation, requires completion

**Issue**:
- AI generation requests get stuck in `Pending` status
- Tasks accumulate in RabbitMQ queue `handle-render-pipeline-attempt-create-request-task`
- No worker to process AI generation requests

**What's Missing**:
- Response handling from AI service
- Result persistence
- Worker infrastructure (Program.cs, Dockerfile, docker-compose)
- Error handling

**Impact**:
- AI generation feature non-functional
- Frontend shows "Waiting for generation..." indefinitely

**ETA**: Weekend (backend developer)

**Temporary Workaround**: None - feature unavailable until completion

---

### Migration History

**Current Migrations**:
1. `20260125073011_Initial` - Complete database schema
2. `20260125073037_AddDefaultData` - Roles, admin user, reference data
3. `20260130000000_AddSystemMediaAndPoseData` - Pose references + system media
4. `20260131000000_AddOutboxTables` - Outbox pattern tables
5. `20260201000000_MakeProductMediaAggregateIdNullable` - Nullable FK fix
6. `20260201120000_RevertToMediaAggregateId` - Revert to required FK

**Removed Migrations**: Old incremental migrations replaced by new Initial

---

## 📝 Contributing

### Code Style
- C#: Follow Microsoft C# coding conventions
- TypeScript: ESLint + Prettier
- Commit messages: Conventional Commits format

### Testing
```bash
# Backend
dotnet test

# Frontend
npm test
```

### Pull Requests
1. Create feature branch from `main`
2. Make changes
3. Update CHANGELOG.md
4. Create PR with detailed description

---

## 📄 License

Proprietary - All Rights Reserved

---

## 👥 Contributors

- **ILya2406** - Backend & Frontend Lead
- **sempersonalacc** - Backend Developer & AI Integration
- **Claude Sonnet 4.5** - AI Assistant (Bug fixes & Documentation)

---

## 🔗 Links

- **Backend Repository**: https://github.com/ILya2406/FashionFace_
- **Frontend Repository**: https://github.com/ILya2406/glamhub-prfile_actual
- **Issue Tracker**: GitHub Issues

---

## 📞 Support

For questions or issues:
1. Check CHANGELOG.md for recent changes
2. Search existing GitHub Issues
3. Create new issue with detailed description

---

**Last Updated**: 2026-02-02
**Version**: 0.1.0-alpha
**Status**: 🚧 Active Development
