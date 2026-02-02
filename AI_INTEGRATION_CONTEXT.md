# AI Integration - Current State & Next Steps

**Context Document for AI Assistant**
*Используй этот документ для быстрого восстановления контекста при продолжении работы*

---

## 📍 Current State (2026-02-02 10:30)

### ✅ What's Working

#### Backend - User Event Worker
**File**: `FashionFace.Executable.Worker.UserEvents/Handlers/HandleRenderPipelineAttemptCreateTaskConsumer.cs`

**Flow** (работает корректно):
1. Пользователь создает `RenderPipeline` через API
2. Создается `RenderPipelineAttempt` с статусом `Pending`
3. Worker получает задачу `HandleRenderPipelineAttemptCreateTask` из RabbitMQ
4. Worker успешно:
   - Загружает Dossier с медиа (✅ исправлен SQL JOIN)
   - Извлекает snap photo (`DossierMediaAggregate`)
   - Находит pose reference по ID
   - Создает новую задачу `HandleRenderPipelineAttemptCreateRequestTask`
   - Отправляет задачу в очередь `handle-render-pipeline-attempt-create-request-task`

**Логи успешного выполнения**:
```
user_event_worker  | Consumed HandleRenderPipelineAttemptCreateTask
user_event_worker  | SELECT d1."Id", d1."IsDeleted", d1."ProfileId", d0."Id", d0."DossierId", d0."MediaAggregateId"
user_event_worker  | FROM (SELECT d."Id", d."IsDeleted", d."ProfileId" FROM "Dossier" AS d WHERE d."ProfileId" = @__profileId)
user_event_worker  | LEFT JOIN "DossierMediaAggregate" AS d0 ON d1."Id" = d0."DossierId"
user_event_worker  | ✅ Task completed successfully
user_event_worker  | SEND rabbitmq://rabbitmq/vhost/handle-render-pipeline-attempt-create-request-task
```

**Fixes Applied Today**:
1. **Dossier Configuration** (`PortfolioConfiguration.cs`):
   - Changed primary key from `ProfileId` to `Id` (через `base.Configure()`)
   - Removed duplicate `IsDeleted` configuration

2. **Navigation Properties Loading**:
   - Added `.Include(entity => entity.Profile)` in `UserDossierMediaListFacade.cs` (line 31-33)
   - Added `.Include(entity => entity.Dossier).ThenInclude(entity => entity!.Profile)` in `UserDossierMediaDeleteFacade.cs` (line 34-38)

#### Frontend - AI Generator
**File**: `glamhub-profile-webapp/frontend/src/pages/AIGenerator.tsx`

**Working**:
- ✅ Загрузка списка поз через `getPoseReferenceList()`
- ✅ Отображение 3D модели позы (Three.js + OBJ loader)
- ✅ Создание `RenderPipeline` через `createRenderPipeline()`
- ✅ Создание `RenderPipelineAttempt` через `createRenderPipelineAttempt()`
- ✅ Polling статуса через `getRenderPipelineAttempt()` (каждые 2 сек)

**Current Behavior**:
- Пользователь выбирает позу → кликает "Generate"
- Frontend показывает "Waiting for generation..." и крутит спиннер
- Статус застревает на `Pending` бесконечно (потому что AI worker не обрабатывает)

---

### ❌ What's Missing - AI Integration Worker

**Location**: `FashionFace.Executable.Worker.Integration.AI/`

**Current State**: Частично реализован, но **НЕ развернут и НЕ работает**

#### Что ЕСТЬ (✅):

**File**: `Handlers/HandleRenderPipelineAttemptCreateRequestTaskConsumer.cs` (строки 1-173)

```csharp
public async Task Consume(ConsumeContext<HandleRenderPipelineAttemptCreateRequestTask> context)
{
    var task = context.Message;

    // ✅ Получение model snap из Dossier
    var dossierMediaAggregate = await genericReadRepository
        .GetCollection<DossierMediaAggregate>()
        .Include(entity => entity.MediaAggregate)
        .ThenInclude(entity => entity!.PreviewMedia)
        .ThenInclude(entity => entity!.OptimizedFile)
        .ThenInclude(entity => entity!.FileResource)
        .FirstOrDefaultAsync(/* ... */);

    // ✅ Получение pose reference
    var poseReferenceMediaAggregate = await genericReadRepository
        .GetCollection<PoseReferenceMediaAggregate>()
        .Include(/* ... */)
        .FirstOrDefaultAsync(/* ... */);

    // ✅ Подготовка HTTP request
    var httpClient = aiServiceHttpClientBuilder.Build();
    var formContent = new MultipartFormDataContent();

    // ✅ Добавление model image
    var modelImageContent = new ByteArrayContent(modelImageBytes);
    modelImageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
    formContent.Add(modelImageContent, "model", "model.jpg");

    // ✅ Добавление pose image
    var poseImageContent = new ByteArrayContent(poseImageBytes);
    poseImageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
    formContent.Add(poseImageContent, "pose", "pose.jpg");

    // ✅ Отправка POST /apply-pose
    var response = await httpClient.PostAsync("/apply-pose", formContent);

    // ❌ ВСЁ! ДАЛЬШЕ НИЧЕГО НЕТ
}
```

**Что работает**:
1. ✅ Consumer подключен к RabbitMQ queue
2. ✅ Загрузка model snap photo из Dossier
3. ✅ Загрузка pose reference image
4. ✅ Формирование multipart/form-data request
5. ✅ Отправка POST к AI-сервису `/apply-pose`

#### Что НЕ СДЕЛАНО (❌):

**1. Response Handling** (строки 174+):
```csharp
// TODO: Нужно добавить после строки 173
if (!response.IsSuccessStatusCode)
{
    // Обработка ошибки AI-сервиса
    var errorContent = await response.Content.ReadAsStringAsync();

    // Создать RenderFailedResult
    var failedResult = new RenderFailedResult
    {
        Id = Guid.NewGuid(),
        RenderPipelineAttemptId = task.RenderPipelineAttemptId,
        ErrorMessage = errorContent
    };
    await createRepository.CreateAsync(failedResult);

    // Обновить статус RenderPipelineAttempt -> Failed
    renderPipelineAttempt.Status = PipelineAttemptStatus.Failed;
    await updateRepository.UpdateAsync(renderPipelineAttempt);

    return;
}

// Успешный ответ - получить изображение
var resultImageBytes = await response.Content.ReadAsByteArrayAsync();
```

**2. Save Result as MediaAggregate**:
```csharp
// TODO: Создать MediaAggregate для результата
// 1. Upload bytes to storage (ImageKit or local)
var fileResource = new FileResource
{
    Id = Guid.NewGuid(),
    RelativePath = uploadedPath, // из ImageKit/storage
    Size = resultImageBytes.Length
};
await createRepository.CreateAsync(fileResource);

// 2. Create MediaFile
var mediaFile = new MediaFile
{
    Id = Guid.NewGuid(),
    FileResourceId = fileResource.Id
};
await createRepository.CreateAsync(mediaFile);

// 3. Create OptimizedFile (опционально, можно тот же FileResource)
// 4. Create PreviewMedia
// 5. Create MediaAggregate
var resultMediaAggregate = new MediaAggregate
{
    Id = Guid.NewGuid(),
    Description = $"AI Generated - {poseReference.Name}",
    PreviewMediaId = previewMedia.Id,
    /* ... */
};
await createRepository.CreateAsync(resultMediaAggregate);
```

**3. Create RenderSucceededResult**:
```csharp
// TODO: Создать успешный результат
var succeededResult = new RenderSucceededResult
{
    Id = Guid.NewGuid(),
    RenderPipelineAttemptId = task.RenderPipelineAttemptId,
    ResultMediaAggregateId = resultMediaAggregate.Id
};
await createRepository.CreateAsync(succeededResult);
```

**4. Update RenderPipelineAttempt Status**:
```csharp
// TODO: Обновить статус
renderPipelineAttempt.Status = PipelineAttemptStatus.Succeeded;
await updateRepository.UpdateAsync(renderPipelineAttempt);
```

**5. Worker Infrastructure** (ПОЛНОСТЬЮ ОТСУТСТВУЕТ):

Нужно создать:

**/FashionFace.Executable.Worker.Integration.AI/Program.cs**:
```csharp
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories
builder.Services.AddScoped<IGenericReadRepository, GenericReadRepository>();
builder.Services.AddScoped<ICreateRepository, CreateRepository>();
builder.Services.AddScoped<IUpdateRepository, UpdateRepository>();

// Add HTTP Client for AI Service
builder.Services.AddHttpClient<IAiServiceHttpClientBuilder, AiServiceHttpClientBuilder>(client =>
{
    var aiServiceUrl = builder.Configuration["AiService:BaseUrl"];
    client.BaseAddress = new Uri(aiServiceUrl);
    client.Timeout = TimeSpan.FromMinutes(5); // AI generation может быть долгой
});

// Add MassTransit with RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<HandleRenderPipelineAttemptCreateRequestTaskConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMq:Host"], h =>
        {
            h.Username(builder.Configuration["RabbitMq:Username"]);
            h.Password(builder.Configuration["RabbitMq:Password"]);
        });

        cfg.ReceiveEndpoint("handle-render-pipeline-attempt-create-request-task", e =>
        {
            e.ConfigureConsumer<HandleRenderPipelineAttemptCreateRequestTaskConsumer>(context);
            e.PrefetchCount = 1; // По одной задаче за раз
        });
    });
});

var host = builder.Build();
await host.RunAsync();
```

**/FashionFace.Executable.Worker.Integration.AI/appsettings.json**:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=postgres;Database=fashionface;Username=postgres;Password=postgres"
  },
  "RabbitMq": {
    "Host": "rabbitmq",
    "Username": "guest",
    "Password": "guest"
  },
  "AiService": {
    "BaseUrl": "http://ai-service:8080"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "MassTransit": "Debug"
    }
  }
}
```

**/FashionFace.Executable.Worker.Integration.AI/Dockerfile**:
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "FashionFace.Executable.Worker.Integration.AI/FashionFace.Executable.Worker.Integration.AI.csproj"
RUN dotnet build "FashionFace.Executable.Worker.Integration.AI/FashionFace.Executable.Worker.Integration.AI.csproj" -c Release -o /app/build
RUN dotnet publish "FashionFace.Executable.Worker.Integration.AI/FashionFace.Executable.Worker.Integration.AI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FashionFace.Executable.Worker.Integration.AI.dll"]
```

**docker-compose.yml** (добавить сервис):
```yaml
ai_worker:
  build:
    context: .
    dockerfile: FashionFace.Executable.Worker.Integration.AI/Dockerfile
  environment:
    - ASPNETCORE_ENVIRONMENT=Development
    - ConnectionStrings__DefaultConnection=Host=postgres;Database=fashionface;Username=postgres;Password=postgres
    - RabbitMq__Host=rabbitmq
    - AiService__BaseUrl=http://ai-service:8080
  depends_on:
    - postgres
    - rabbitmq
  networks:
    - fashionface-network
  restart: unless-stopped
```

---

## 🎯 Next Steps for Backend Developer

### 1. Complete Consumer Logic (Priority: HIGH)

**File**: `HandleRenderPipelineAttemptCreateRequestTaskConsumer.cs`

**После строки 173** добавить:
- [ ] Response validation (`response.IsSuccessStatusCode`)
- [ ] Error handling → create `RenderFailedResult`
- [ ] Success handling → extract image bytes
- [ ] Upload result to storage (ImageKit or local)
- [ ] Create `FileResource` + `MediaFile` + `MediaAggregate`
- [ ] Create `RenderSucceededResult`
- [ ] Update `RenderPipelineAttempt.Status` (Succeeded/Failed)

**Estimated Time**: 2-3 hours

### 2. Create Worker Infrastructure (Priority: HIGH)

- [ ] `Program.cs` with MassTransit + RabbitMQ setup
- [ ] `appsettings.json` with configuration
- [ ] `Dockerfile`
- [ ] Add service to `docker-compose.yml`

**Estimated Time**: 1-2 hours

### 3. Test End-to-End (Priority: MEDIUM)

- [ ] Deploy AI worker
- [ ] Verify RabbitMQ queue consumption
- [ ] Test successful generation flow
- [ ] Test error handling (AI service down, invalid input, etc.)
- [ ] Verify Frontend receives result

**Estimated Time**: 1 hour

---

## 🧪 How to Test When Ready

### 1. Проверить Worker запущен

```bash
docker compose ps | grep ai_worker
# Должен быть "Up"
```

### 2. Проверить RabbitMQ Queue

Открыть http://localhost:15672 → Queues → должен быть:
- `handle-render-pipeline-attempt-create-request-task` (consumers: 1)

### 3. Frontend Test Flow

1. Зайти на http://localhost:5173/ai-generator
2. Выбрать позу из списка
3. Кликнуть "Generate"
4. Ждать 10-30 секунд (зависит от AI-сервиса)
5. **Expected Result**: статус меняется Pending → Processing → Succeeded
6. Увидеть сгенерированное изображение

### 4. Check Logs

```bash
# Worker logs
docker compose logs ai_worker -f

# Expected logs:
# - "Consumed HandleRenderPipelineAttemptCreateRequestTask"
# - "Sending request to AI service POST /apply-pose"
# - "AI service responded: 200 OK"
# - "Created RenderSucceededResult"
# - "Updated RenderPipelineAttempt status: Succeeded"
```

### 5. Database Verification

```sql
-- Check RenderPipelineAttempt status
SELECT * FROM "RenderPipelineAttempt" ORDER BY "CreatedAt" DESC LIMIT 10;
-- Status должен быть "Succeeded" или "Failed", НЕ "Pending"

-- Check Results
SELECT * FROM "RenderSucceededResult" ORDER BY "CreatedAt" DESC LIMIT 10;
-- Должны быть записи с ResultMediaAggregateId

-- Check Generated MediaAggregates
SELECT * FROM "MediaAggregate" WHERE "Description" LIKE 'AI Generated%';
```

---

## 🐛 Known Issues & Workarounds

### Issue 1: Tasks Stuck in Pending
**Symptom**: `RenderPipelineAttempt.Status` остается `Pending` навсегда
**Cause**: AI worker не обрабатывает задачи (не развернут)
**Workaround**: Нет - нужно завершить реализацию

### Issue 2: Queue Backlog
**Symptom**: RabbitMQ queue `handle-render-pipeline-attempt-create-request-task` накапливает сообщения
**Cause**: Consumer не настроен
**Workaround**: Очистить очередь через RabbitMQ Management UI → Queues → Purge

### Issue 3: Frontend Infinite Loading
**Symptom**: Spinner крутится бесконечно
**Cause**: Polling получает Pending всегда
**Workaround**: Добавить timeout на фронте (например, 2 минуты → показать "Generation timeout")

---

## 📞 Contact Context

**Backend Developer**: sempersonalacc
**Status**: "На выходных доделаю" (ETA: Weekend)
**Communication**: Сообщение в Telegram/Discord

---

## 🤖 For AI Assistant (Claude)

### Quick Resume Instructions

**When user says**: "Бэк доделал AI worker, проверь работает ли"

**Your action**:
1. Read this file to restore context
2. Ask user to run: `docker compose ps | grep ai_worker`
3. Check RabbitMQ Management UI (consumers on queue)
4. Ask user to test generation from frontend
5. Check logs: `docker compose logs ai_worker -f`
6. Verify database records (use SQL queries above)

**When user says**: "Помоги доделать Consumer"

**Your action**:
1. Read this file section "What's Missing - Response Handling"
2. Open `HandleRenderPipelineAttemptCreateRequestTaskConsumer.cs`
3. Add code after line 173 as documented above
4. Test compilation: `dotnet build FashionFace.Executable.Worker.Integration.AI`

### Key Files to Remember
- Consumer: `FashionFace.Executable.Worker.Integration.AI/Handlers/HandleRenderPipelineAttemptCreateRequestTaskConsumer.cs`
- Config: Dossier fixes in `FashionFace.Repositories.Context/Configurations/DossierEntities/PortfolioConfiguration.cs`
- Frontend: `glamhub-profile-webapp/frontend/src/pages/AIGenerator.tsx`
- Documentation: `CHANGELOG.md`, `README.md`

---

**Document Version**: 1.0
**Last Updated**: 2026-02-02 10:30
**Status**: 🚧 Waiting for Backend Developer Completion
