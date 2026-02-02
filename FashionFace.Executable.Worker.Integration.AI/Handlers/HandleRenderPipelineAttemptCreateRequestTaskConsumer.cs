using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using FashionFace.Common.Extensions.Implementations;
using FashionFace.Common.Models.Models.Commands;
using FashionFace.Dependencies.HttpClient.Interfaces;
using FashionFace.Dependencies.MimeDetective.Interfaces;
using FashionFace.Repositories.Context.Models.MediaEntities;
using FashionFace.Repositories.Context.Models.TaskEntity;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Repositories.Strategy.Builders.Args;
using FashionFace.Repositories.Strategy.Builders.Interfaces;
using FashionFace.Repositories.Strategy.Interfaces;
using FashionFace.Services.ConfigurationSettings.Interfaces;
using FashionFace.Services.Singleton.Args;
using FashionFace.Services.Singleton.Interfaces;

using MassTransit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FashionFace.Executable.Worker.Integration.AI.Handlers;

public sealed class HandleRenderPipelineAttemptCreateRequestTaskConsumer(
    ITaskBatchStrategy taskBatchStrategy,
    ICorrelatedSelectPendingStrategyBuilder correlatedSelectPendingStrategyBuilder,
    IDangerousHttpClientBuilder dangerousHttpClientBuilder,
    IAiServiceSettingsFactory aiServiceSettingsFactory,
    IMimeDetectiveDecorator mimeDetectiveDecorator,
    IFileStorageOpenReadService  fileStorageOpenReadService,
    IGenericReadRepository genericReadRepository,
    ILogger<HandleRenderPipelineAttemptCreateRequestTaskConsumer> logger
) : IConsumer<HandleRenderPipelineAttemptCreateRequestTask>
{
    private const int BatchSize = 5;

    public async Task Consume(
        ConsumeContext<HandleRenderPipelineAttemptCreateRequestTask> context
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
            nameof(HandleRenderPipelineAttemptCreateRequestTaskConsumer);

        logger
            .LogInformation(
                $"Consumer {taskConsumerName} started for {eventMessage.CorrelationId}"
            );

        var strategyArgs =
            new CorrelatedSelectPendingStrategyBuilderArgs(
                eventMessage.CorrelationId,
                BatchSize
            );

        var batchArgs =
            correlatedSelectPendingStrategyBuilder
                .Build<RenderPipelineAttemptCreateRequestTask>(
                    strategyArgs
                );

        var taskList =
            await
                taskBatchStrategy
                    .ClaimBatchAsync<RenderPipelineAttemptCreateRequestTask>(
                        batchArgs
                    );

        while (taskList.IsNotEmpty())
        {
            foreach (var task in taskList)
            {
                await
                    HandleTask(
                        task
                    );

                await
                    taskBatchStrategy
                        .MakeDoneAsync(
                            task
                        );
            }

            taskList =
                await
                    taskBatchStrategy
                        .ClaimBatchAsync<RenderPipelineAttemptCreateRequestTask>(
                            batchArgs
                        );
        }

        logger
            .LogInformation(
                $"Consumer {taskConsumerName} ended for {eventMessage.CorrelationId}"
            );
    }

    private async Task HandleTask(
        RenderPipelineAttemptCreateRequestTask task
    )
    {
        var modelByteArray =
            await
                GetByteArray(
                    task.ModelMediaAggregateId
                );

        var poseByteArray =
            await
                GetByteArray(
                    task.PoseReferenceMediaAggregateId
                );

        using var formContent =
            new MultipartFormDataContent();

        formContent.Add(
            new ByteArrayContent(modelByteArray),
            name: "person_image",
            fileName: "person.jpg"
        );

        formContent.Add(
            new ByteArrayContent(poseByteArray),
            name: "pose_image",
            fileName: "pose.png"
        );

        formContent
            .Add(
                new StringContent(
                    task.Temperature.ToString()
                ),
                "temperature"
            );

        formContent
            .Add(
                new StringContent(
                    task.UserPrompt
                ),
                "custom_prompt"
            );

        formContent.Headers.ContentType =
            new("multipart/form-data");

        var httpClient =
            GetHttpClient();

        var response =
            await
                httpClient
                    .PostAsync(
                        "/apply-pose",
                        formContent
                    );
    }

    private HttpClient GetHttpClient()
    {
        var aiServiceSettings =
            aiServiceSettingsFactory.GetSettings();

        var baseUrl =
            aiServiceSettings.BaseUrl;

        var httpClient =
            dangerousHttpClientBuilder
                .Build(
                    baseUrl
                );

        return
            httpClient;
    }

    private async Task<byte[]> GetByteArray(
        Guid mediaAggregateId
    )
    {
        var relativePath =
            await
                GetRelevantPath(
                    mediaAggregateId
                );

        var byteArray =
            await
                GetFileResourceByteArray(
                    relativePath
                );

        return
            byteArray;
    }

    private async Task<byte[]> GetFileResourceByteArray(
        string modelRelativePath
    )
    {
        var modelFileStorageOpenReadServiceArgs =
            new FileStorageOpenReadServiceArgs(
                modelRelativePath
            );

        var modelImageBytes =
            await
                fileStorageOpenReadService
                    .OpenReadAsync(
                        modelFileStorageOpenReadServiceArgs
                    );

        using var modelMemoryStream =
            new MemoryStream();

        await
            modelImageBytes
                .CopyToAsync(
                    modelMemoryStream
                );

        var modelByteArray =
            modelMemoryStream.ToArray();

        return
            modelByteArray;
    }

    private async Task<string> GetRelevantPath(
        Guid mediaAggregateId
    )
    {
        var mediaAggregateCollection =
            genericReadRepository.GetCollection<MediaAggregate>();

        var relevantPathModel =
            await
                mediaAggregateCollection
                    .Select(
                        entity =>
                            new {
                                entity.Id,
                                entity
                                    .OriginalMedia!
                                    .OriginalFile!
                                    .FileResource!
                                    .RelativePath,
                            }
                    )
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == mediaAggregateId
                    );

        var relativePath =
            relevantPathModel!.RelativePath;

        return
            relativePath;
    }
}