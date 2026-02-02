# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased] - 2026-02-02

### Added

#### Backend - AI Render Pipeline Infrastructure (🚧 In Progress)
- **RenderPipeline Domain Models**
  - `RenderPipeline` - основная модель пайплайна рендеринга
  - `RenderPipelineAttempt` - попытка рендеринга с отслеживанием статуса
  - `RenderSucceededResult` / `RenderFailedResult` - результаты рендеринга
  - `PipelineAttemptStatus` enum (Pending, Processing, Succeeded, Failed)

- **PoseReference System**
  - `PoseReference` - справочник поз для AI генерации
  - `PoseReferenceMediaAggregate` - медиа-контент для поз
  - Controllers + Facades для работы с позами через API
  - Миграция с дефолтными позами (`20260130000000_AddSystemMediaAndPoseData`)

- **Task Management System**
  - `ITask` interface для задач в очереди
  - `TaskStatus` enum для отслеживания статусов
  - `HandleRenderPipelineAttemptCreateTask` - создание попытки рендеринга
  - `HandleRenderPipelineAttemptCreateRequestTask` - отправка запроса к AI-сервису

- **Event Publishing Infrastructure**
  - `IEventPublishService` - публикация событий в MassTransit
  - Integration с RabbitMQ для event-driven архитектуры

- **Worker Infrastructure**
  - `HandleRenderPipelineAttemptCreateTaskConsumer` в `Worker.UserEvents`
  - Обработка создания RenderPipelineAttempt
  - Отправка задачи в очередь для AI worker

#### Frontend - AI Generator & Profile Improvements
- **AI Generator Page** (`AIGenerator.tsx`)
  - Интеграция с PoseReference API
  - Загрузка и отображение доступных поз
  - Создание RenderPipeline через FashionFace API
  - Polling статуса рендеринга (Pending → Succeeded/Failed)

- **Profile Snap Photos Management**
  - Интеграция с Dossier API для загрузки/удаления снэпов
  - Исправлена логика удаления (маппинг `id` вместо `mediaId`)
  - Перезагрузка списка с сервера после добавления/удаления
  - Исправлена проблема: снэпы теперь сохраняются после рефреша

- **FashionFace API Client** (`fashionface-client.ts`)
  - `uploadSnapToDossier()` - загрузка снэпа в досье
  - `getDossierMediaList()` - получение списка медиа из досье
  - `deleteDossierMedia()` - удаление медиа из досье
  - `createRenderPipeline()` / `createRenderPipelineAttempt()` - AI рендеринг
  - `getRenderPipelineAttempt()` - проверка статуса рендеринга
  - `getPoseReferenceList()` - получение списка поз

- **Pose Viewer Component** (`PoseAngleViewer.tsx`)
  - Отображение 3D моделей поз (`.obj` файлы)
  - Добавлена модель `yoga-tree-pose.obj` с превью

### Fixed

#### Backend - Critical Bugs
- **Dossier Configuration** (`PortfolioConfiguration.cs`)
  - ✅ Исправлен первичный ключ: `ProfileId` → `Id` (через `base.Configure()`)
  - ✅ Удалены дублирующие настройки `IsDeleted` (уже в базовом классе)
  - Теперь Dossier использует стандартный `EntityBase.Id` как PK

- **Navigation Properties Loading**
  - `UserDossierMediaListFacade.cs`: добавлен `.Include(entity => entity.Profile)` перед использованием `entity.Profile!.ApplicationUserId`
  - `UserDossierMediaDeleteFacade.cs`: добавлен `.Include(entity => entity.Dossier).ThenInclude(entity => entity!.Profile)` перед проверкой прав доступа
  - **Проблема**: без Include EF Core не загружал связанные сущности → NullReferenceException
  - **Решение**: явная загрузка навигационных свойств через `.Include()` / `.ThenInclude()`

#### Frontend - Critical Bugs
- **Snap Photos не сохранялись после рефреша**
  - Проблема: при загрузке снэпа сохранялся только URL в локальном состоянии, но не в БД
  - Решение: после `uploadSnapToDossier()` перезагружаем список с сервера через `getDossierMediaList()`

- **Удаление Snap Photos не работало**
  - Проблема 1: сравнение по URL не находило совпадения (локальный blob URL vs серверный URL)
  - Проблема 2: API возвращает поле `id`, а код проверял `mediaId`
  - Решение: получаем актуальный список с сервера, ищем по URL, используем `item.id` для удаления

### Changed

#### Backend - Refactoring
- **EntityConfigurationBase** - улучшена базовая конфигурация для всех сущностей
- **DossierMediaConfiguration** - обновлена для работы с новым PK
- **MediaFile Model** - добавлены дополнительные поля для медиа

- **Outbox Pattern**
  - Обновлены интерфейсы `IOutbox`, `IWithOutboxStatus`
  - `OutboxStatus` enum расширен
  - Миграция `20260131000000_AddOutboxTables.cs` - добавлены таблицы Outbox

#### Migrations
- **Removed Old Migrations** (replaced by new Initial)
  - `20260110172915_Initial`
  - `20260110182844_AddDefaultRoles`
  - `20260110182913_AddDefaultAdmin`
  - `20260110182938_AddAppearanceTraitsDimensions`
  - `20260112135121_UpdateClaimedAtProperty`
  - `20260112175906_AddNotificationOutboxEntities`

- **New Migration Structure**
  - `20260125073011_Initial.cs` - полная начальная схема БД
  - `20260125073037_AddDefaultData.cs` - роли, админ, справочники
  - `20260130000000_AddSystemMediaAndPoseData.cs` - системные медиа + позы
  - `20260131000000_AddOutboxTables.cs` - таблицы для Outbox pattern
  - `20260201000000_MakeProductMediaAggregateIdNullable.cs`
  - `20260201120000_RevertToMediaAggregateId.cs`

---

## 🚧 Work In Progress - Requires Backend Completion

### AI Integration Worker (`FashionFace.Executable.Worker.Integration.AI`)

**Status**: Partial implementation, waiting for backend developer to complete

**What's Done** ✅:
- Created project structure
- `HandleRenderPipelineAttemptCreateRequestTaskConsumer.cs` (partial)
  - Receives task from RabbitMQ queue `handle-render-pipeline-attempt-create-request-task`
  - Fetches model snap from Dossier MediaAggregate
  - Fetches pose reference media
  - Prepares `multipart/form-data` request
  - Sends POST to AI service `/apply-pose` endpoint

**What's Missing** ❌:
- Response handling from AI service
- Parsing result (image / error)
- Saving result as MediaAggregate
- Creating FileResource for result
- Updating RenderPipelineAttempt status (Succeeded/Failed)
- Creating RenderSucceededResult/RenderFailedResult
- Full Worker infrastructure:
  - `Program.cs` with MassTransit + RabbitMQ setup
  - `appsettings.json` with AI service URL configuration
  - Dockerfile
  - docker-compose.yml integration
- Error handling, retry logic, timeout handling

**Impact**:
- AI generation requests get stuck in `Pending` status indefinitely
- Tasks pile up in RabbitMQ queue `handle-render-pipeline-attempt-create-request-task`
- Frontend shows "Waiting for generation..." forever

**Next Steps** (Backend Developer):
1. Complete response handling in Consumer
2. Implement result persistence
3. Create full Worker application infrastructure
4. Test end-to-end AI generation flow
5. Add to docker-compose.yml

**ETA**: Weekend (planned by backend developer)

---

## Infrastructure

### Database
- **Status**: ✅ All migrations created and applied
- **Schema**: PostgreSQL with UUID primary keys
- **Snapshot**: `ApplicationDatabaseContextModelSnapshot.cs` up-to-date

### Docker
- **Backend Services**:
  - ✅ `web_api` - REST API (port 5000)
  - ✅ `user_event_worker` - обработка пользовательских событий
  - ✅ `postgres` - база данных
  - ✅ `rabbitmq` - очереди сообщений
  - ✅ `redis_stack` - кэширование
  - ✅ `hubs` - SignalR hubs
  - ❌ `ai_worker` - **НЕ НАСТРОЕН** (ждем доработки бэка)

- **Frontend**:
  - ✅ Dev server на `localhost:5173`
  - ✅ Интеграция с FashionFace API (`localhost:5000`)

### API Endpoints (New)
```
GET  /api/v1/anonymous/pose-reference/list - список поз
GET  /api/v1/user/dossier/media/list       - медиа из досье
POST /api/v1/user/dossier/media            - добавить медиа в досье
DELETE /api/v1/user/dossier/media          - удалить медиа из досье
POST /api/v1/user/render-pipeline          - создать пайплайн рендеринга
POST /api/v1/user/render-pipeline/attempt  - создать попытку рендеринга
GET  /api/v1/user/render-pipeline/attempt  - получить статус попытки
```

---

## Known Issues

1. **AI Worker не развернут** - задачи висят в Pending
   - Временное решение: ждем завершения реализации от бэк-разработчика

2. **Frontend: старое API `localhost:8000` используется для consent**
   - Вызывает ошибки `ERR_CONNECTION_REFUSED`
   - TODO: полностью мигрировать на FashionFace API

---

## Contributors
- Backend: ILya2406, sempersonalacc
- Frontend: ILya2406
- AI Integration: sempersonalacc (in progress)
- Bug fixes & Integration: Claude Sonnet 4.5 (AI Assistant)

---

## Repository Links
- Backend: https://github.com/ILya2406/FashionFace_
- Frontend: https://github.com/ILya2406/glamhub-prfile_actual
