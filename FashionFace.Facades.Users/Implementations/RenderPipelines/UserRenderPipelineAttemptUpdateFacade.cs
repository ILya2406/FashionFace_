using System.Threading.Tasks;

using FashionFace.Common.Models.Models;
using FashionFace.Dependencies.MassTransit.Interfaces;
using FashionFace.Facades.Users.Args.RenderPipelines;
using FashionFace.Facades.Users.Interfaces.RenderPipelines;
using FashionFace.Repositories.Context.Models.RenderPipelines;
using FashionFace.Repositories.Interfaces;

namespace FashionFace.Facades.Users.Implementations.RenderPipelines;

public sealed class UserRenderPipelineAttemptUpdateFacade(
    IBulkUpdateRepository bulkUpdateRepository,
    IEventPublishService eventPublishService
) : IUserRenderPipelineAttemptUpdateFacade
{
    public async Task Execute(
        UserRenderPipelineAttemptUpdateArgs args
    )
    {
        var (userId, attemptId, userPrompt) = args;

        await
            bulkUpdateRepository
                .ExecuteUpdateAsync<RenderPipelineAttempt>(
                    entity =>
                        entity.ApplicationUserId == userId
                        && entity.Id == attemptId,
                    entity =>
                        entity
                            .SetProperty(
                                attempt =>
                                    attempt
                                        .RenderAttemptSettings!
                                        .UserPrompt,
                                userPrompt
                            )
                );

        var @event =
            new UserRenderPipelineAttemptUpdatedEventModel(
                attemptId,
                userId
            );

        await
            eventPublishService
                .PublishAsync(
                    @event
                );
    }
}