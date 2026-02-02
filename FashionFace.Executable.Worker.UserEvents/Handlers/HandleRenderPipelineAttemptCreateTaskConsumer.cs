using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Models.Models.Commands;
using FashionFace.Dependencies.MassTransit.Interfaces;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.DossierEntities;
using FashionFace.Repositories.Context.Models.TaskEntity;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Context.Models.Talents;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using MassTransit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TaskStatus = FashionFace.Repositories.Context.Enums.TaskStatus;

namespace FashionFace.Executable.Worker.UserEvents.Handlers;

public sealed class HandleRenderPipelineAttemptCreateTaskConsumer(
    IGuidGenerator guidGenerator,
    IDateTimePicker dateTimePicker,
    IBulkUpdateRepository bulkUpdateRepository,
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor,
    IUpdateRepository updateRepository,
    ICreateRepository createRepository,
    ITransactionManager transactionManager,
    ICommandSendService commandSendService,
    ILogger<HandleRenderPipelineAttemptCreateTaskConsumer> logger
) : IConsumer<HandleRenderPipelineAttemptCreateTask>
{
    public async Task Consume(
        ConsumeContext<HandleRenderPipelineAttemptCreateTask> context
    )
    {
        var eventMessage =
            context.Message;

        using var loggerScope =
            logger
                .BeginScope(
                    new
                    {
                        eventMessage.CorrelationId,
                    }
                );

        var taskConsumerName =
            nameof(HandleRenderPipelineAttemptCreateTaskConsumer);

        logger
            .LogInformation(
                $"Consumer {taskConsumerName} started for {eventMessage.CorrelationId}"
            );

        var updatedCount =
            await
                bulkUpdateRepository
                    .ExecuteUpdateAsync<RenderPipelineAttemptCreateTask>(
                        entity =>
                            entity.CorrelationId == eventMessage.CorrelationId
                            && entity.TaskStatus == TaskStatus.Pending,
                        entity =>
                            entity.SetProperty(
                                task => task.TaskStatus,
                                TaskStatus.Claimed
                            )
                    );

        if (updatedCount == 0)
        {
            logger
                .LogInformation(
                    "Nothing to handle"
                );

            return;
        }

        var renderPipelineAttemptCreateTaskCollection =
            genericReadRepository.GetCollection<RenderPipelineAttemptCreateTask>();

        var task =
            await
                renderPipelineAttemptCreateTaskCollection
                    .FirstOrDefaultAsync(
                        entity => entity.CorrelationId == eventMessage.CorrelationId
                    );

        if (task is null)
        {
            throw exceptionDescriptor.Exception(
                "NothingToHandle"
            );
        }

        var correlationId =
            eventMessage.CorrelationId;

        var renderPipelineAttemptCollection =
            genericReadRepository.GetCollection<RenderPipelineAttempt>();

        var renderPipelineAttempt =
            await
                renderPipelineAttemptCollection

                    .Include(
                        entity => entity.RenderAttemptSettings!
                    )
                    .ThenInclude(
                        entity => entity.PoseReferenceProjection
                    )

                    .Include(
                        entity => entity.RenderPipeline
                    )

                    .FirstOrDefaultAsync(
                        entity => entity.Id == task.RenderPipelineAttemptId
                    );

        if (renderPipelineAttempt is null)
        {
            throw exceptionDescriptor.NotFound<RenderPipelineAttempt>();
        }

        var pipelineAttemptSettings =
            renderPipelineAttempt.RenderAttemptSettings!;

        var poseReferenceMediaAggregateId =
            pipelineAttemptSettings
                .PoseReferenceProjection!
                .MediaAggregateId;

        var productMediaAggregateId =
            renderPipelineAttempt
                .RenderPipeline!
                .ProductMediaAggregateId;

        var talentId =
            renderPipelineAttempt
                .RenderPipeline!
                .TalentId;

        var talentCollection =
            genericReadRepository.GetCollection<Talent>();

        var talent =
            await
                talentCollection
                    .Include(entity => entity.ProfileTalent)
                    .FirstOrDefaultAsync(
                        entity => entity.Id == talentId
                    );

        var dossierCollection =
            genericReadRepository.GetCollection<Dossier>();

        var dossier =
            await
                dossierCollection
                    .Include(entity => entity.DossierMediaCollection)
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ProfileId ==
                            talent!
                                .ProfileTalent!
                                .ProfileId
                    );

        var dossierMediaCollection =
            dossier!.DossierMediaCollection;

        var modelMediaAggregateId =
            dossierMediaCollection
                .First()
                .MediaAggregateId;

        var renderPipelineAttemptCreateRequestTask =
            new RenderPipelineAttemptCreateRequestTask
            {
                Id = guidGenerator.GetNew(),
                InitiatorUserId = task.InitiatorUserId,
                RenderPipelineAttemptId =  task.RenderPipelineAttemptId,
                ModelMediaAggregateId = modelMediaAggregateId,
                ProductMediaAggregateId = productMediaAggregateId,
                PoseReferenceMediaAggregateId = poseReferenceMediaAggregateId,
                UserPrompt = pipelineAttemptSettings.UserPrompt,
                Temperature = pipelineAttemptSettings.Temperature,

                CreatedAt = dateTimePicker.GetUtcNow(),
                CorrelationId = correlationId,
                AttemptCount = 0,
                TaskStatus = TaskStatus.Pending,
                ClaimedAt = null,
            };

        using var transaction =
            await
                transactionManager.BeginTransaction();

        await
            createRepository
                .CreateAsync(
                    renderPipelineAttemptCreateRequestTask
                );

        task.TaskStatus = TaskStatus.Done;

        await
            updateRepository
                .UpdateAsync(
                    task
                );

        await
            transaction.CommitAsync();

        var command =
            new HandleRenderPipelineAttemptCreateRequestTask(
                correlationId
            );

        await
            commandSendService
                .SendAsync(
                    command
                );

        logger
            .LogInformation(
                $"Consumer {taskConsumerName} ended successfully for {eventMessage.CorrelationId}"
            );
    }
}