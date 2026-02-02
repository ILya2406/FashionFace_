using System.Threading.Tasks;

using FashionFace.Common.Models.Models.Commands;
using FashionFace.Dependencies.MassTransit.Interfaces;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;
using FashionFace.Facades.Users.Models.RenderPipelines;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.TaskEntity;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.EntityFrameworkCore;

using TaskStatus = FashionFace.Repositories.Context.Enums.TaskStatus;

namespace FashionFace.Facades.Users.Implementations.RenderPipelines;

public sealed class UserRenderPipelineAttemptCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    ITransactionManager transactionManager,
    IGuidGenerator guidGenerator,
    IDateTimePicker dateTimePicker,
    ICommandSendService commandSendService
) : IUserRenderPipelineAttemptCreateFacade
{
    public async Task<UserRenderPipelineAttemptCreateResult> Execute(
        UserRenderPipelineAttemptCreateArgs args
    )
    {
        var (userId, renderPipelineId, poseReferenceMediaAggregateId, userPrompt, temperature) = args;

        var userPromptHash =
            userPrompt.GetHashCode();

        var renderPipelineAttemptCollection =
            genericReadRepository.GetCollection<RenderPipelineAttempt>();

        var renderPipelineAttempt =
            await
                renderPipelineAttemptCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ApplicationUserId == userId
                            && entity.RenderPipelineId == renderPipelineId
                            && entity.RenderAttemptSettings!.Temperature.ToString() == temperature.ToString()
                            && entity.RenderAttemptSettings.UserPromptHash == userPromptHash
                            && entity.RenderAttemptSettings.PoseReferenceProjection!.MediaAggregateId ==  poseReferenceMediaAggregateId
                    );

        if (renderPipelineAttempt is not null)
        {
            var existedResult =
                new UserRenderPipelineAttemptCreateResult(
                    renderPipelineAttempt.Id
                );

            return
                existedResult;
        }

        var renderPipelineCollection =
            genericReadRepository.GetCollection<RenderPipeline>();

        var renderPipeline =
            await
                renderPipelineCollection
                    .FirstOrDefaultAsync(
                        entity => entity.Id == renderPipelineId
                    );

        var poseReferenceId =
            renderPipeline!.PoseReferenceId;

        var renderPipelineAttemptSettingsId =
            guidGenerator.GetNew();

        var poseReferenceProjectionId =
            guidGenerator.GetNew();

        var poseReferenceProjection =
            new PoseReferenceProjection
            {
                Id =  poseReferenceProjectionId,
                MediaAggregateId = poseReferenceMediaAggregateId,
                PoseReferenceId = poseReferenceId,
            };

        var renderPipelineAttemptSettings =
            new RenderPipelineAttemptSettings
            {
                Id = renderPipelineAttemptSettingsId,
                UserPrompt =  userPrompt,
                Temperature = temperature,
                UserPromptHash =  userPromptHash,
                PoseReferenceProjectionId = poseReferenceProjectionId,
                PoseReferenceProjection = poseReferenceProjection,
            };

        var renderPipelineAttemptId =
            guidGenerator.GetNew();

        var newRenderPipelineAttempt =
            new RenderPipelineAttempt
            {
                Id = renderPipelineAttemptId,
                ApplicationUserId = userId,
                IsDeleted =  false,
                Status = PipelineAttemptStatus.Pending,
                CreatedAt = dateTimePicker.GetUtcNow(),
                FinishedAt = null,
                StartedAt = null,
                RenderPipelineId = renderPipelineId,
                RenderAttemptSettingsId = renderPipelineAttemptSettingsId,
                RenderAttemptSettings = renderPipelineAttemptSettings,
            };

        var correlationId =
            guidGenerator.GetNew();

        var task =
            new RenderPipelineAttemptCreateTask
            {
                Id = guidGenerator.GetNew(),
                InitiatorUserId = userId,
                RenderPipelineAttemptId = renderPipelineAttemptId,

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
                    newRenderPipelineAttempt
                );

        await
            createRepository
                .CreateAsync(
                    task
                );

        await
            transaction.CommitAsync();

        var command =
            new HandleRenderPipelineAttemptCreateTask(
                task.CorrelationId
            );

        await
            commandSendService
                .SendAsync(
                    command
                );

        var result =
            new UserRenderPipelineAttemptCreateResult(
                newRenderPipelineAttempt.Id
            );

        return
            result;
    }
}